using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Dados.Interfaces
{
    public interface ISubCategoriasRepository
    {
        SubCategorias BuscarPorID(int categoriaId, int subCategoriaId, int usuarioId);

        IEnumerable<SubCategorias> BuscarTodosPorCategoria(int categoriaId, int usuarioId);

        int Inserir(SubCategorias objeto);

        int Atualizar(SubCategorias objeto, int usuarioId);

        int Excluir(int categoriaId, int subCategoriaId, int usuarioId);
    }
}
