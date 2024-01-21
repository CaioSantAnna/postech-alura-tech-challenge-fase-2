using GerenciadorAcervo.Dados;
using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.Funcoes;
using GerenciadorAcervo.Modelos.Tabelas;
using GerenciadorAcervo.Servicos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Servicos
{
    public class SubCategoriasService : BaseService, ISubCategoriasService
    {
        private IUnitOfWork _unitOfWork;        

        public SubCategoriasService(IUnitOfWork unitOfWork, RetornoApi retorno, IHttpContextAccessor httpContext) : base(retorno, httpContext)
        {
            _unitOfWork = unitOfWork; 
        }

        public bool AtualizarSubCategoria(AlteracaoSubCategoriaRequest objeto)
        {
            if (!String.IsNullOrWhiteSpace(objeto.Imagem))
                if (!Funcoes.EhImagemValida(objeto.Imagem))
                {
                    AtualizarRetorno("Valor informado para o campo imagem inválido. Verifique se a imagem é uma string base64 válida");
                    return false;
                }

            int retorno = _unitOfWork.SubCategorias.Atualizar(new SubCategorias(objeto), usuarioIdUsuarioLogado);
            _unitOfWork.Commit();
            if (retorno == 0) 
            {
                AtualizarRetorno("Categoria/subcategoria inexistente para o usuário logado. Nenhuma informação foi atualizada");
                return false;
            }   

            return true;
        }
            

        public IEnumerable<SubCategorias> BuscarTodosPorCategoria(int categoriaId)
            => _unitOfWork.SubCategorias.BuscarTodosPorCategoria(categoriaId, usuarioIdUsuarioLogado);

        public int CadastrarSubCategoria(CadastroSubCategoriaRequest objeto)
        {
            if (!String.IsNullOrWhiteSpace(objeto.Imagem))
                if (!Funcoes.EhImagemValida(objeto.Imagem))
                {
                    AtualizarRetorno("Valor informado para o campo imagem inválido. Verifique se a imagem é uma string base64 válida");
                    return 0;
                }

            if (_unitOfWork.Categorias.BuscarPorID(objeto.CategoriaId, usuarioIdUsuarioLogado) == null)
            {
                AtualizarRetorno("Categoria inexistente ou não válida para o usuário logado. Nenhuma informação foi incluída");
                return 0;
            }   

            int subCategoriaId = _unitOfWork.SubCategorias.Inserir(new SubCategorias(objeto));
            _unitOfWork.Commit();

            return subCategoriaId;
        }

        public bool ExcluirSubCategoria(int categoriaId, int subCategoriaId)
        {
            int retorno = _unitOfWork.SubCategorias.Excluir(categoriaId, subCategoriaId, usuarioIdUsuarioLogado);
            _unitOfWork.Commit();
            if (retorno == 0) 
            {
                AtualizarRetorno("Categoria/subcategoria inexistente para o usuário logado. Nenhuma informação foi excluida");
                return false;
            }
            return true;
        }
    }
}
