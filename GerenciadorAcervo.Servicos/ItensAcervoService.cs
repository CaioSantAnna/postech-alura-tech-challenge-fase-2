using GerenciadorAcervo.Dados;
using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.DTOs.Retornos;
using GerenciadorAcervo.Modelos.Enums;
using GerenciadorAcervo.Modelos.Funcoes;
using GerenciadorAcervo.Modelos.Tabelas;
using GerenciadorAcervo.Servicos.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Servicos
{
    public class ItensAcervoService : BaseService, IItensAcervoService
    {
        private IUnitOfWork _unitOfWork;
        public ItensAcervoService(IUnitOfWork unitOfWork, RetornoApi retorno, IHttpContextAccessor httpContext) : base(retorno, httpContext)
        {
            _unitOfWork = unitOfWork;
        }

        public int InserirItemAcervo(CadastroItemAcervoRequest request)
        {
            List<int> listaIdsPropriedadesInvalidas = new();

            if (request.AtributosValores.Any())
            {
                List<int> listaIdsCadastro = request.AtributosValores.GroupBy(x => x.AtributoId).Select(x => x.Key).ToList();
                List<Atributos> atributosCadastroBanco = _unitOfWork.Atributos.BuscarTodosPorListaIds(listaIdsCadastro.ToArray(), usuarioIdUsuarioLogado).ToList();

                if (listaIdsCadastro.Except(atributosCadastroBanco.Select(x => x.AtributoId)).Any())
                {
                    AtualizarRetorno($"As propriedades {String.Join(",", listaIdsCadastro.Except(atributosCadastroBanco.Select(x => x.AtributoId)).Select(x => x))} são inválidas para a categoria/subcategoria do item que está sendo cadastro no momento. Nenhuma informação foi incluída.");
                    return 0;
                }

                foreach (CadastroItemAcervoAtributoValorRequest item in request.AtributosValores)
                {
                    if (!Funcoes.EhAtributoValorValido((AtributoTipoEnum)atributosCadastroBanco.SingleOrDefault(x => x.AtributoId == item.AtributoId).AtributoTipoId, item.Valor))
                        listaIdsPropriedadesInvalidas.Add(item.AtributoId);
                }

                if (listaIdsPropriedadesInvalidas.Any())
                {
                    AtualizarRetorno($"As propriedades {String.Join(",", listaIdsPropriedadesInvalidas)} possuem valores inválidos considerando o tipo de atributo cadastrado.");
                    return 0;
                }
            }

            if (_unitOfWork.Categorias.BuscarPorID(request.CategoriaId, usuarioIdUsuarioLogado) == null)
            {
                AtualizarRetorno("Categoria inexistente ou não válida para o usuário logado. Nenhuma informação foi incluída");
                return 0;
            }

            if (request.SubCategoriaId != null)
                if (_unitOfWork.SubCategorias.BuscarPorID(request.CategoriaId, Convert.ToInt32(request.SubCategoriaId), usuarioIdUsuarioLogado) == null)
                {
                    AtualizarRetorno("SubCategoria inexistente ou não válida para o usuário logado. Nenhuma informação foi incluída");
                    return 0;
                }

            if (request.ContatoId != null)
                if (!_unitOfWork.Contatos.EhContatoValido(Convert.ToInt32(request.ContatoId), usuarioIdUsuarioLogado))
                {
                    AtualizarRetorno("Contato inexistente ou não válido para o usuário logado. Nenhuma informação foi incluída");
                    return 0;
                }

            int itemAcervoId = _unitOfWork.ItensAcervo.Inserir(new ItensAcervo(request, usuarioIdUsuarioLogado));
            foreach (CadastroItemAcervoAtributoValorRequest item in request.AtributosValores)
            {
                _unitOfWork.ItensAcervoAtributosValores.Inserir(new ItensAcervoAtributosValores(item, itemAcervoId));
            }
            _unitOfWork.Commit();

            return itemAcervoId;
        }

        public bool AtualizarItemAcervo(AlteracaoItemAcervoRequest request)
        {
            List<int> listaIdsPropriedadesInvalidas = new();

            if (request.AtributosValores.Any())
            {
                ItensAcervo itemAcervo = _unitOfWork.ItensAcervo.Buscar(new ItensAcervo(request, usuarioIdUsuarioLogado));
                List<int> listaIdsCadastro = request.AtributosValores.GroupBy(x => x.AtributoId).Select(x => x.Key).ToList();
                List<Atributos> atributosCadastroBanco = _unitOfWork.Atributos.BuscarTodosPorCategoria(request.CategoriaId, usuarioIdUsuarioLogado).ToList();

                if (request.SubCategoriaId != null)
                    atributosCadastroBanco.AddRange(_unitOfWork.Atributos.BuscarTodosPorSubCategoria(request.CategoriaId, Convert.ToInt32(request.SubCategoriaId), usuarioIdUsuarioLogado).ToList());

                if (itemAcervo == null)
                {
                    AtualizarRetorno($"CategoriaId e/ou subCategoriaId inválidas para o produto atual. Essas informações não podem ser trocadas. Informe os dados originais.");
                    return false;
                }

                if (listaIdsCadastro.Except(atributosCadastroBanco.Select(x => x.AtributoId)).Any())
                {
                    AtualizarRetorno($"As propriedades {String.Join(",", listaIdsCadastro.Except(atributosCadastroBanco.Select(x => x.AtributoId)).Select(x => x))} são inválidas para a categoria/subcategoria do item que está sendo alterado no momento. Nenhuma informação foi alterada.");
                    return false;
                }

                foreach (CadastroItemAcervoAtributoValorRequest item in request.AtributosValores)
                {
                    if (!Funcoes.EhAtributoValorValido((AtributoTipoEnum)atributosCadastroBanco.SingleOrDefault(x => x.AtributoId == item.AtributoId).AtributoTipoId, item.Valor))
                        listaIdsPropriedadesInvalidas.Add(item.AtributoId);
                }

                if (listaIdsPropriedadesInvalidas.Any())
                {
                    AtualizarRetorno($"As propriedades {String.Join(",", listaIdsPropriedadesInvalidas)} possuem valores inválidos considerando o tipo de atributo informado.");
                    return false;
                }
            }

            if (_unitOfWork.Categorias.BuscarPorID(request.CategoriaId, usuarioIdUsuarioLogado) == null)
            {
                AtualizarRetorno("Categoria inexistente ou não válida para o usuário logado. Nenhuma informação foi alterada");
                return false;
            }

            if (request.SubCategoriaId != null)
                if (_unitOfWork.SubCategorias.BuscarPorID(request.CategoriaId, Convert.ToInt32(request.SubCategoriaId), usuarioIdUsuarioLogado) == null)
                {
                    AtualizarRetorno("SubCategoria inexistente ou não válida para o usuário logado. Nenhuma informação foi alterada");
                    return false;
                }

            if (request.ContatoId != null)
                if (!_unitOfWork.Contatos.EhContatoValido(Convert.ToInt32(request.ContatoId), usuarioIdUsuarioLogado))
                {
                    AtualizarRetorno("Contato inexistente ou não válido para o usuário logado. Nenhuma informação foi incluída");
                    return false;
                }

            int retorno = _unitOfWork.ItensAcervo.Atualizar(new ItensAcervo(request, usuarioIdUsuarioLogado));
            foreach (CadastroItemAcervoAtributoValorRequest item in request.AtributosValores)
            {
                _unitOfWork.ItensAcervoAtributosValores.Atualizar(new ItensAcervoAtributosValores(item, request.ItemAcervoId), request.CategoriaId, usuarioIdUsuarioLogado);
            }
            _unitOfWork.Commit();

            if (retorno == 0)
            {
                AtualizarRetorno("Item inexistente para o usuário logado. Nenhuma informação foi atualizada");
                return false;
            }

            return true;
        }

        public RetornoPaginado<ItemAcervoRetorno> BuscarComFiltros(PaginacaoItensAcervoRequest objeto)
        {
            RetornoPaginado<ItemAcervoRetorno> retornoPaginado = new RetornoPaginado<ItemAcervoRetorno>();

            List<ItemAcervoRetorno> itensAcervo = new List<ItemAcervoRetorno>();
            List<ItensAcervo> itensPesquisa = _unitOfWork.ItensAcervo.BuscarComFiltros(objeto, usuarioIdUsuarioLogado).ToList();
            List<Contatos> contatos = _unitOfWork.Contatos.BuscarTodos(usuarioIdUsuarioLogado).ToList();
            List<Atributos> atributos = _unitOfWork.Atributos.BuscarTodos(usuarioIdUsuarioLogado).ToList();

            itensPesquisa.GroupJoin(contatos, u => u.ContatoId, p => p.ContatoId, (itensAcervo, contatos) => new { itensAcervo, contatos })
            .SelectMany(x => x.contatos.DefaultIfEmpty(),
                (t, p) => new
                {
                    itemAcervo = t.itensAcervo,
                    contato = p
                }).ToList().ForEach(x =>
                {
                    List<ItensAcervoAtributosValores> atributosItemAcervo = _unitOfWork.ItensAcervoAtributosValores.BuscarPorItemAcervoId(x.itemAcervo.ItemAcervoId, x.itemAcervo.CategoriaId, usuarioIdUsuarioLogado).ToList();

                    itensAcervo.Add(
                        new ItemAcervoRetorno
                        {
                            ItemAcervoId = x.itemAcervo.ItemAcervoId,
                            Nome = x.itemAcervo.Nome,
                            CategoriaId = x.itemAcervo.CategoriaId,
                            SubCategoriaId = x.itemAcervo.SubCategoriaId,
                            ContatoId = x.contato?.ContatoId,
                            ContatoNome = x.contato?.Nome,
                            EstahEmprestado = x.contato?.ContatoId == null ? false : true,
                            AtributosValores = atributosItemAcervo.Join(atributos,
                                               atributosItemAcervo => atributosItemAcervo.AtributoId,
                                               atributos => atributos.AtributoId,
                                                    (atributosItemAcervo, atributos) =>
                                                        new ItemAcervoAtributoValorRetorno
                                                        {
                                                            AtributoId = atributos.AtributoId,
                                                            Nome = atributos.Nome,
                                                            Descricao = atributos.Descricao,
                                                            Valor = atributosItemAcervo.Valor,
                                                            AtributoTipoId = atributos.AtributoTipoId
                                                        }).ToList()
                        });
                });

            retornoPaginado.Itens = itensAcervo;
            retornoPaginado.ItensTotal = Convert.ToInt32(itensPesquisa.FirstOrDefault()?.Total);

            return retornoPaginado;
        }

        public bool ExcluirItemAcervo(int itemAcervoId)
        {
            int retorno = _unitOfWork.ItensAcervo.Excluir(itemAcervoId, usuarioIdUsuarioLogado);
            _unitOfWork.Commit();

            if (retorno == 0)
            {
                AtualizarRetorno("Item inexistente para o usuário logado. Nenhuma informação foi excluida");
                return false;
            }

            return true;
        }
    }
}
