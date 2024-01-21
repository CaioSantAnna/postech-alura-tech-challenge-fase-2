using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.DTOs.Retornos;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Servicos.Interfaces
{
    public interface IContatosService
    {
        int CadastrarContato(CadastroContatoRequest objeto);
        bool AtualizarContato(AlteracaoContatoRequest objeto);
        bool ExcluirContato(int contatoId);
        RetornoPaginado<Contatos> BuscarComFiltros(PaginacaoRequest objeto);
    }
}
