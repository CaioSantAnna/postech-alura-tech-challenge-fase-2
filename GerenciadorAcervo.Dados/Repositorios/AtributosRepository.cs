using Dapper;
using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GerenciadorAcervo.Dados.Repositorios
{
    public class AtributosRepository : BaseRepository, IAtributosRepository
    {
        public AtributosRepository(IDbTransaction transacao) : base(transacao)
        {
        }

        public int Atualizar(Atributos objeto, int usuarioId)
        {
            return Conexao.Execute(@"UPDATE A
                                     SET    A.Nome = @Nome,
                                            A.Descricao = @Descricao
                                     FROM   Atributos A
                                            INNER JOIN Categorias B
                                                    ON A.CategoriaId = B.CategoriaId
                                            LEFT JOIN SubCategorias C
                                                   ON A.CategoriaId = C.CategoriaId
                                                      AND A.SubCategoriaId = C.SubCategoriaId
                                     WHERE  A.CategoriaId = @CategoriaId
                                            AND A.SubCategoriaId" + (objeto.SubCategoriaId == null ? " IS NULL " : " = @SubCategoriaId ") +
                                          @"AND A.AtributoId = @AtributoId
                                            AND B.UsuarioId = @usuarioId", new { objeto.Nome, objeto.Descricao, objeto.CategoriaId, objeto.SubCategoriaId, objeto.AtributoId, usuarioId } , Transacao);
        }

        public IEnumerable<Atributos> BuscarTodosPorCategoria(int categoriaId, int usuarioId)
        {
            var query = @"SELECT B.*
                          FROM   Categorias A
                                 INNER JOIN Atributos B
                                         ON A.CategoriaId = B.CategoriaId
                          WHERE  A.CategoriaId = @categoriaId
                                 AND A.UsuarioId = @usuarioId
                                 AND B.SubCategoriaId IS NULL
                          ORDER  BY B.Nome ";

            return Conexao.Query<Atributos>(query, new { categoriaId, usuarioId }, Transacao);
        }

        public IEnumerable<Atributos> BuscarTodosPorSubCategoria(int categoriaId, int subcategoriaId, int usuarioId)
        {
            var query = @"SELECT C.*
                          FROM   Categorias A
                                 INNER JOIN SubCategorias B
                                         ON A.CategoriaId = B.CategoriaId
                                 INNER JOIN Atributos C
                                         ON A.CategoriaId = C.CategoriaId
                                            AND B.SubCategoriaId = C.SubCategoriaId
                          WHERE  A.CategoriaId = @categoriaId
                                 AND B.SubCategoriaId = @subCategoriaId
                                 AND A.UsuarioId = @usuarioId
                          ORDER  BY C.Nome ";

            return Conexao.Query<Atributos>(query, new { categoriaId, subcategoriaId, usuarioId }, Transacao);
        }

        public IEnumerable<Atributos> BuscarTodosPorListaIds(int[] listaIds, int usuarioId)
        {
            var query = @"SELECT B.*
                          FROM   Categorias A
                                 INNER JOIN Atributos B
                                         ON A.CategoriaId = B.CategoriaId
                          WHERE  A.UsuarioId = @usuarioId AND B.AtributoId IN @listaIds
                          ORDER  BY B.Nome ";

            return Conexao.Query<Atributos>(query, new { listaIds, usuarioId }, Transacao);
        }

        public IEnumerable<Atributos> BuscarTodos(int usuarioId)
        {
            var query = @"SELECT B.*
                          FROM   Categorias A
                                 INNER JOIN Atributos B
                                         ON A.CategoriaId = B.CategoriaId
                          WHERE  A.UsuarioId = @usuarioId
                          ORDER  BY B.Nome ";

            return Conexao.Query<Atributos>(query, new { usuarioId }, Transacao);
        }

        public bool EhAtributoValido(int categoriaId, int? subCategoriaId, int usuarioId)
        {
            string query = @"SELECT Iif(EXISTS(SELECT 1
                             FROM   Categorias A
                             WHERE  A.CategoriaId = @categoriaId
                                    AND A.UsuarioId = @usuarioId
                             UNION ALL
                             SELECT 1
                             FROM   Categorias A
                                    INNER JOIN SubCategorias B
                                            ON A.CategoriaId = B.CategoriaId
                             WHERE  A.CategoriaId = @categoriaId
                                    AND B.SubCategoriaId = @subCategoriaId
                                    AND A.UsuarioId = @usuarioId), 1, 0)";

            return Conexao.Query<bool>(query, new { categoriaId, subCategoriaId, usuarioId }, Transacao).SingleOrDefault();
        }

        public int Excluir(int categoriaId, int? subCategoriaId, int atributoId, int usuarioId)
        {
            return Conexao.Execute(@"DELETE A
                                     FROM   Atributos A
                                            INNER JOIN Categorias B
                                                    ON A.CategoriaId = B.CategoriaId
                                            LEFT JOIN SubCategorias C
                                                   ON A.CategoriaId = C.CategoriaId
                                                      AND A.SubCategoriaId = C.SubCategoriaId
                                     WHERE  A.CategoriaId = @categoriaId
                                            AND A.SubCategoriaId" + (subCategoriaId == null ? " IS NULL " : " = @subCategoriaId ") +
                                          @"AND A.AtributoId = @atributoId
                                            AND B.UsuarioId = @usuarioId", new { categoriaId, subCategoriaId, atributoId, usuarioId }, Transacao);
        }

        public int Inserir(Atributos objeto)
        {
            objeto.AtributoId = Conexao.ExecuteScalar<int>(@"INSERT INTO Atributos
                                                                         (AtributoTipoId,
                                                                          CategoriaId,
                                                                          SubCategoriaId,
                                                                          Nome,
                                                                          Descricao)
                                                             VALUES      (@AtributoTipoId,
                                                                          @CategoriaId,
                                                                          @SubCategoriaId,
                                                                          @Nome,
                                                                          @Descricao); SELECT SCOPE_IDENTITY() ", objeto, Transacao);

            return objeto.AtributoId;
        }
    }
}
