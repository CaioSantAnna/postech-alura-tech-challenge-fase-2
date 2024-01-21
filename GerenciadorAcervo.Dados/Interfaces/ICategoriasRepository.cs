using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Dados.Interfaces
{
    public interface ICategoriasRepository
    {
        Categorias BuscarPorID(int categoriaId, int usuarioId);
        IEnumerable<Categorias> BuscarTodos(int usuarioId);
        int Inserir(Categorias objeto);
        int Atualizar(Categorias objeto);
        int Excluir(int categoriaId, int usuarioId);
        IEnumerable<Categorias> BuscarComFiltros(PaginacaoRequest objeto, int usuarioId);
    }
}
