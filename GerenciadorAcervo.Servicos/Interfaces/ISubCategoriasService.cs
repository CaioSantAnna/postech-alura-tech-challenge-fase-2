using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Servicos.Interfaces
{
    public interface ISubCategoriasService
    {
        int CadastrarSubCategoria(CadastroSubCategoriaRequest objeto);
        bool AtualizarSubCategoria(AlteracaoSubCategoriaRequest objeto);
        bool ExcluirSubCategoria(int categoriaId, int subCategoriaId);
        IEnumerable<SubCategorias> BuscarTodosPorCategoria(int categoriaId);
    }
}
