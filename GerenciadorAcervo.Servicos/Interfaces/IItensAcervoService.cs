using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.DTOs.Retornos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Servicos.Interfaces
{
    public interface IItensAcervoService
    {
        int InserirItemAcervo(CadastroItemAcervoRequest request);
        RetornoPaginado<ItemAcervoRetorno> BuscarComFiltros(PaginacaoItensAcervoRequest objeto);
        bool ExcluirItemAcervo(int itemAcervoId);
        bool AtualizarItemAcervo(AlteracaoItemAcervoRequest request);
    }
}
