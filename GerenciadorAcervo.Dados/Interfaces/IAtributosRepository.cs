using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Dados.Interfaces
{
    public interface IAtributosRepository
    {
        IEnumerable<Atributos> BuscarTodosPorCategoria(int categoriaId, int usuarioId);
        IEnumerable<Atributos> BuscarTodosPorSubCategoria(int categoriaId, int subcategoriaId, int usuarioId);
        int Inserir(Atributos objeto);
        int Atualizar(Atributos objeto, int usuarioId);
        int Excluir(int categoriaId, int? subCategoriaId, int atributoId, int usuarioId);
        bool EhAtributoValido(int categoriaId, int? subCategoriaId, int usuarioId);
        IEnumerable<Atributos> BuscarTodosPorListaIds(int[] listaIds, int usuarioId);
        IEnumerable<Atributos> BuscarTodos(int usuarioId);
    }
}
