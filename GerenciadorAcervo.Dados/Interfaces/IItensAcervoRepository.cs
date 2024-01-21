using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Dados.Interfaces
{
    public interface IItensAcervoRepository
    {
        int Atualizar(ItensAcervo objeto);

        int Excluir(int itemAcervoId, int usuarioId);

        int Inserir(ItensAcervo objeto);

        IEnumerable<ItensAcervo> BuscarComFiltros(PaginacaoItensAcervoRequest objeto, int usuarioId);

        ItensAcervo Buscar(ItensAcervo objeto);
    }
}
