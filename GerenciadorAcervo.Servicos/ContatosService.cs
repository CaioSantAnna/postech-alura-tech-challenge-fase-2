using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.DTOs.Retornos;
using GerenciadorAcervo.Modelos.Funcoes;
using GerenciadorAcervo.Modelos.Tabelas;
using GerenciadorAcervo.Servicos.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GerenciadorAcervo.Servicos
{
    public class ContatosService : BaseService, IContatosService
    {
        private IUnitOfWork _unitOfWork;

        public ContatosService(IUnitOfWork unitOfWork, RetornoApi retornoApi, IHttpContextAccessor httpContext) : base(retornoApi, httpContext)
        {
            _unitOfWork = unitOfWork;            
        }

        public bool AtualizarContato(AlteracaoContatoRequest objeto)
        {
            int retorno = _unitOfWork.Contatos.Atualizar(new Contatos(objeto, usuarioIdUsuarioLogado));
            _unitOfWork.Commit();

            if (retorno == 0) 
            {
                AtualizarRetorno("Contato inexistente para o usuário logado. Nenhuma informação foi atualizada");
                return false;
            }
            return true;
        }

        public RetornoPaginado<Contatos> BuscarComFiltros(PaginacaoRequest objeto)
        {
            RetornoPaginado<Contatos> retornoPaginado = new RetornoPaginado<Contatos>();

            List<Contatos> contatos = new List<Contatos>();
            contatos = _unitOfWork.Contatos.BuscarComFiltros(objeto, usuarioIdUsuarioLogado).ToList();

            retornoPaginado.Itens = contatos;
            retornoPaginado.ItensTotal = retornoPaginado.ItensTotal = Convert.ToInt32(contatos.FirstOrDefault()?.Total);

            return retornoPaginado;
        }

        public int CadastrarContato(CadastroContatoRequest objeto)
        {
            int idContato;
            idContato = _unitOfWork.Contatos.Inserir(new Contatos(objeto, Convert.ToInt32(base.usuarioIdUsuarioLogado)));
            _unitOfWork.Commit();

            return idContato;
        }

        public bool ExcluirContato(int contatoId)
        {
            int retorno = _unitOfWork.Contatos.Excluir(contatoId, usuarioIdUsuarioLogado); ;
            _unitOfWork.Commit();

            if (retorno == 0) 
            {
                AtualizarRetorno("Contato inexistente para o usuário logado. Nenhuma informação foi excluida");
                return false;
            }
            return true;
        }
    }
}
