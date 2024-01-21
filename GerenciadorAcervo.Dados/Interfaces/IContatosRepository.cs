using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Dados.Interfaces
{
    public interface IContatosRepository
    {
        int Atualizar(Contatos objeto);
        int Excluir(int contatoId, int usuarioId);
        public IEnumerable<Contatos> BuscarComFiltros(PaginacaoRequest objeto, int usuarioId);
        int Inserir(Contatos objeto);
        bool EhContatoValido(int contatoId, int usuarioId);
        IEnumerable<Contatos> BuscarTodos(int usuarioId);
    }
}
