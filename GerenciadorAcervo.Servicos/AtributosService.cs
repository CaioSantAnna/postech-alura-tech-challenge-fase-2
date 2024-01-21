using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Modelos.DTOs.Requests;
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
    public class AtributosService : BaseService, IAtributosService
    {
        private IUnitOfWork _unitOfWork;
        public AtributosService(IUnitOfWork unitOfWork, RetornoApi retorno, IHttpContextAccessor httpContext) : base(retorno, httpContext)
        {
            _unitOfWork = unitOfWork;
        }

        public bool AtualizarAtributo(AlteracaoAtributoRequest objeto)
        {
            int retorno = _unitOfWork.Atributos.Atualizar(new Atributos(objeto), usuarioIdUsuarioLogado);
            _unitOfWork.Commit();

            if (retorno == 0) 
            {
                AtualizarRetorno("Atributo inválido para o usuário logado. Nenhuma informação foi atualizada");
                return false;
            }   

            return true;
        }

        public IEnumerable<Atributos> BuscarTodosPorCategoria(int categoriaId)
            => _unitOfWork.Atributos.BuscarTodosPorCategoria(categoriaId, usuarioIdUsuarioLogado);

        public IEnumerable<Atributos> BuscarTodosPorSubCategoria(int categoriaId, int subcategoriaId)
            => _unitOfWork.Atributos.BuscarTodosPorSubCategoria(categoriaId, subcategoriaId, usuarioIdUsuarioLogado);

        public int CadastrarAtributo(CadastroAtributoRequest objeto)
        {
            if (!_unitOfWork.Atributos.EhAtributoValido(objeto.CategoriaId, objeto.SubCategoriaId, usuarioIdUsuarioLogado))
            {
                AtualizarRetorno("Cadastro de atributo inválido para o usuário logado. Nenhuma informação foi incluída.");
                return 0;
            }

            int subCategoriaId = _unitOfWork.Atributos.Inserir(new Atributos(objeto));
            _unitOfWork.Commit();

            return subCategoriaId;
        }

        public bool ExcluirAtributo(int categoriaId, int? subCategoriaId, int atributoId)
        {
            int retorno = _unitOfWork.Atributos.Excluir(categoriaId, subCategoriaId, atributoId, usuarioIdUsuarioLogado);
            _unitOfWork.Commit();
            if (retorno == 0)
            {
                AtualizarRetorno("Atributo inválido para o usuário logado. Nenhuma informação foi excluida");
                return false;
            }
                

            return true;
        }
    }
}
