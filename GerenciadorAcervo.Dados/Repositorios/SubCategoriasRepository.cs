using Dapper;
using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Dados.Repositorios
{
    public class SubCategoriasRepository : BaseRepository, ISubCategoriasRepository
    {
        public SubCategoriasRepository(IDbTransaction transacao) : base(transacao)
        {
        }

        public int Atualizar(SubCategorias objeto, int usuarioId)
        {
            return Conexao.Execute(@"UPDATE A
                                     SET    A.Nome = @Nome,
                                            A.Descricao = @Descricao,
                                            A.Imagem = @Imagem
                                     FROM   SubCategorias A
                                            INNER JOIN Categorias B
                                                    ON A.CategoriaId = B.CategoriaId
                                     WHERE  A.SubCategoriaId = @SubCategoriaId
                                            AND A.CategoriaId = @CategoriaId
                                            AND B.UsuarioId = @usuarioId", new { objeto.Nome, objeto.Descricao, objeto.Imagem, objeto.SubCategoriaId, objeto.CategoriaId, usuarioId }, Transacao);
        }

        public SubCategorias BuscarPorID(int categoriaId, int subCategoriaId, int usuarioId)
        {
            var query = @"SELECT A.*
                          FROM   SubCategorias A
                                 INNER JOIN Categorias B
                                         ON A.CategoriaId = B.CategoriaId
                          WHERE  A.SubCategoriaId = @subCategoriaId
                                 AND A.CategoriaId = @categoriaId
                                 AND B.UsuarioId = @usuarioId
                          ORDER  BY A.Nome";

            return Conexao.Query<SubCategorias>(query, new { categoriaId, subCategoriaId, usuarioId }, Transacao).SingleOrDefault();
        }

        public IEnumerable<SubCategorias> BuscarTodosPorCategoria(int categoriaId, int usuarioId)
        {
            var query = @"SELECT A.*
                          FROM   SubCategorias A
                                 INNER JOIN Categorias B
                                         ON A.CategoriaId = B.CategoriaId
                          WHERE  A.CategoriaId = @categoriaId
                                 AND B.UsuarioId = @usuarioId
                          ORDER  BY A.Nome";

            return Conexao.Query<SubCategorias>(query, new { categoriaId, usuarioId }, Transacao);
        }

        public int Excluir(int categoriaId, int subCategoriaId, int usuarioId)
        {
            return Conexao.Execute(@"DELETE A
                                     FROM   SubCategorias A
                                            INNER JOIN Categorias B
                                                    ON A.CategoriaId = B.CategoriaId
                                     WHERE  A.SubCategoriaId = @subCategoriaId
                                            AND A.CategoriaId = @categoriaId
                                            AND B.UsuarioId = @usuarioId", new { categoriaId, subCategoriaId, usuarioId }, Transacao);
        }

        public int Inserir(SubCategorias objeto)
        {
            objeto.SubCategoriaId = Conexao.ExecuteScalar<int>(@"INSERT INTO SubCategorias
                                                             (CategoriaId,
                                                              Nome,
                                                              Descricao,
                                                              Imagem)
                                                             VALUES
                                                             (@CategoriaId,
                                                              @Nome,
                                                              @Descricao,
                                                              @Imagem); SELECT SCOPE_IDENTITY() ", objeto, Transacao);

            return objeto.SubCategoriaId;
        }
    }
}
