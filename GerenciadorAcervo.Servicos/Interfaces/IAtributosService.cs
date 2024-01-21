using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Servicos.Interfaces
{
    public interface IAtributosService
    {
        int CadastrarAtributo(CadastroAtributoRequest objeto);
        bool AtualizarAtributo(AlteracaoAtributoRequest objeto);
        bool ExcluirAtributo(int categoriaId, int? subCategoriaId, int atributoId);
        IEnumerable<Atributos> BuscarTodosPorCategoria(int categoriaId);
        IEnumerable<Atributos> BuscarTodosPorSubCategoria(int categoriaId, int subcategoriaId);
    }
}
