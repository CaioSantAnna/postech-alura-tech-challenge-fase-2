using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Dados.Interfaces
{
    public interface IItensAcervoAtributosValoresRepository
    {
        void Atualizar(ItensAcervoAtributosValores objeto, int categoriaId, int usuarioId);
        void Inserir(ItensAcervoAtributosValores objeto);
        IEnumerable<ItensAcervoAtributosValores> BuscarPorItemAcervoId(int itemAcervoId, int categoriaId, int usuarioId);
    }
}
