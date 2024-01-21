using Dapper;
using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.Tabelas;
using System.Data;
using System.Linq;
using System.Text;

namespace GerenciadorAcervo.Dados.Repositorios
{
    public class CategoriasRepository : BaseRepository, ICategoriasRepository
    {
        public CategoriasRepository(IDbTransaction transacao) : base(transacao)
        {
        }

        public int Atualizar(Categorias objeto)
        {
            return Conexao.Execute(@"UPDATE Categorias SET Nome = @Nome, Descricao = @Descricao, Imagem = @Imagem WHERE CategoriaId = @CategoriaId AND UsuarioId = @UsuarioId", objeto, Transacao);
        }

        public IEnumerable<Categorias> BuscarComFiltros(PaginacaoRequest objeto, int usuarioId)
        {
            int offset = (objeto.NumeroPagina - 1) * objeto.QuantidadeItensPorPagina;
            StringBuilder where = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(objeto.TextoPesquisa))
                where.Append($" AND Nome LIKE CONCAT('%',@TextoPesquisa,'%')");

            var query = @"SELECT *, Count(*) OVER () AS Total
                          FROM   Categorias
                          WHERE UsuarioId = @usuarioId [whereComplemento]
                          ORDER  BY Nome
                          OFFSET @offset ROWS FETCH NEXT @QuantidadeItensPorPagina ROWS ONLY";

            return Conexao.Query<Categorias>(query.Replace("[whereComplemento]", where.ToString()), new { offset, objeto.QuantidadeItensPorPagina, usuarioId, objeto.TextoPesquisa}, Transacao);
        }

        public Categorias BuscarPorID(int categoriaId, int usuarioId)
        {
            var query = @"SELECT *
                          FROM   Categorias
                          WHERE UsuarioId = @usuarioId
                                AND CategoriaId = @categoriaId
                          ORDER  BY Nome";

            return Conexao.Query<Categorias>(query, new { usuarioId, categoriaId }, Transacao).SingleOrDefault();
        }

        public IEnumerable<Categorias> BuscarTodos(int usuarioId)
        {
            var query = @"SELECT *
                          FROM   Categorias
                          WHERE UsuarioId = @usuarioId
                          ORDER  BY Nome";

            return Conexao.Query<Categorias>(query, new { usuarioId }, Transacao);            
        }

        public int Excluir(int categoriaId, int usuarioId)
        {
            return Conexao.Execute(@"DELETE FROM Categorias 
                              WHERE CategoriaId = @categoriaId AND UsuarioId = @usuarioId", new {categoriaId, usuarioId}, Transacao);
        }

        public int Inserir(Categorias objeto)
        {
            objeto.CategoriaId = Conexao.ExecuteScalar<int>(@"INSERT INTO Categorias
                                                             (UsuarioId,
                                                              Nome,
                                                              Descricao,
                                                              Imagem)
                                                             VALUES
                                                             (@UsuarioId,
                                                              @Nome,
                                                              @Descricao,
                                                              @Imagem); SELECT SCOPE_IDENTITY() ", objeto, Transacao);

            return objeto.CategoriaId;
        }
    }
}
