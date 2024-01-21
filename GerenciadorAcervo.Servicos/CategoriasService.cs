using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.DTOs.Retornos;
using GerenciadorAcervo.Modelos.Funcoes;
using GerenciadorAcervo.Modelos.Tabelas;
using GerenciadorAcervo.Servicos.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GerenciadorAcervo.Servicos
{
    public class CategoriasService : BaseService, ICategoriasService
    {
        private IUnitOfWork _unitOfWork;

        public CategoriasService(IUnitOfWork unitOfWork, RetornoApi retornoApi, IHttpContextAccessor httpContext) : base(retornoApi, httpContext)
        {
            _unitOfWork = unitOfWork;            
        }

        public bool AtualizarCategoria(AlteracaoCategoriaRequest objeto)
        {
            if (!String.IsNullOrWhiteSpace(objeto.Imagem))
                if (!Funcoes.EhImagemValida(objeto.Imagem)) 
                {
                    AtualizarRetorno("Valor informado para o campo imagem inválido. Verifique se a imagem é uma string base64 válida");
                    return false;
                }

            int retorno = _unitOfWork.Categorias.Atualizar(new Categorias(objeto, usuarioIdUsuarioLogado));
            _unitOfWork.Commit();

            if (retorno == 0) 
            {
                AtualizarRetorno("Categoria inexistente para o usuário logado. Nenhuma informação foi atualizada");
                return false;
            }   

            return true;
        }

        public RetornoPaginado<Categorias> BuscarComFiltros(PaginacaoRequest objeto)
        {
            RetornoPaginado<Categorias> retornoPaginado = new RetornoPaginado<Categorias>();

            List<Categorias> categorias = new List<Categorias>();
            categorias = _unitOfWork.Categorias.BuscarComFiltros(objeto, usuarioIdUsuarioLogado).ToList();

            categorias.ForEach(x => {
                x.SubCategorias = _unitOfWork.SubCategorias.BuscarTodosPorCategoria(x.CategoriaId, usuarioIdUsuarioLogado).ToList();
            });

            retornoPaginado.Itens = categorias;
            retornoPaginado.ItensTotal = Convert.ToInt32(categorias.FirstOrDefault()?.Total);

            return retornoPaginado;
        }

        public int CadastrarCategoria(CadastroCategoriaRequest objeto)
        {
            if (!String.IsNullOrWhiteSpace(objeto.Imagem))
                if (!Funcoes.EhImagemValida(objeto.Imagem))
                {
                    AtualizarRetorno("Valor informado para o campo imagem inválido. Verifique se a imagem é uma string base64 válida");
                    return 0;
                }

            int idCategoria;
            idCategoria = _unitOfWork.Categorias.Inserir(new Categorias(objeto, usuarioIdUsuarioLogado));
            _unitOfWork.Commit();

            return idCategoria;
        }

        public bool ExcluirCategoria(int categoriaId)
        {
            int retorno = _unitOfWork.Categorias.Excluir(categoriaId, usuarioIdUsuarioLogado); ;
            _unitOfWork.Commit();

            if (retorno == 0) 
            {
                AtualizarRetorno("Categoria inexistente para o usuário logado. Nenhuma informação foi excluida");
                return false;
            }   

            return true;
        }   
    }
}
