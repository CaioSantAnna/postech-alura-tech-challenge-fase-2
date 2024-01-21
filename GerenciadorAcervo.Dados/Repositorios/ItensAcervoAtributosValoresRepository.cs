using Dapper;
using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Dados.Repositorios
{
    public class ItensAcervoAtributosValoresRepository : BaseRepository, IItensAcervoAtributosValoresRepository
    {
        public ItensAcervoAtributosValoresRepository(IDbTransaction transacao) : base(transacao)
        {
            
        }

        public void Atualizar(ItensAcervoAtributosValores objeto, int categoriaId, int usuarioId)
        {
            Conexao.Execute(@"IF EXISTS (SELECT *
                                         FROM   ItensAcervoAtributosValores A
                                                INNER JOIN Atributos B
                                                        ON A.AtributoId = B.AtributoId
                                                INNER JOIN Categorias C
                                                        ON B.CategoriaId = C.CategoriaId
                                         WHERE  A.AtributoId = @AtributoId
                                                AND C.CategoriaId = @CategoriaId
                                                AND C.UsuarioId = @UsuarioId
                                                AND A.ItemAcervoId = @ItemAcervoId)
                                BEGIN
                                    UPDATE A
                                    SET    A.Valor = @Valor
                                    FROM   ItensAcervoAtributosValores A
                                           INNER JOIN Atributos B
                                                   ON A.AtributoId = B.AtributoId
                                           INNER JOIN Categorias C
                                                   ON B.CategoriaId = C.CategoriaId
                                    WHERE  A.AtributoId = @AtributoId
                                           AND A.ItemAcervoId = @ItemAcervoId
                                           AND C.CategoriaId = @CategoriaId
                                           AND C.UsuarioId = @UsuarioId
                                END
                              ELSE
                                BEGIN
                                    INSERT INTO ItensAcervoAtributosValores
                                                (ItemAcervoId,
                                                 AtributoId,
                                                 Valor)
                                    VALUES      (@ItemAcervoId,
                                                 @AtributoId,
                                                 @Valor)
                                END", new { objeto.ItemAcervoId, objeto.AtributoId, objeto.Valor, CategoriaId = categoriaId, UsuarioId = usuarioId }, Transacao);
        }

        public void Inserir(ItensAcervoAtributosValores objeto)
        {
            Conexao.ExecuteScalar<int>(@"INSERT INTO ItensAcervoAtributosValores
                                                                           (ItemAcervoId,
                                                                            AtributoId,
                                                                            Valor)
                                                               VALUES      (@ItemAcervoId,
                                                                            @AtributoId,
                                                                            @Valor); SELECT SCOPE_IDENTITY() ", objeto, Transacao);
        }

        public IEnumerable<ItensAcervoAtributosValores> BuscarPorItemAcervoId(int itemAcervoId, int categoriaId, int usuarioId)
        {
            var query = @"SELECT *
                          FROM   ItensAcervoAtributosValores A
                                 INNER JOIN Atributos B
                                         ON A.AtributoId = B.AtributoId
                                 INNER JOIN Categorias C
                                         ON B.CategoriaId = C.CategoriaId
                          WHERE  C.CategoriaId = @CategoriaId
                                 AND C.UsuarioId = @UsuarioId
                                 AND A.ItemAcervoId = @ItemAcervoId";

            return Conexao.Query<ItensAcervoAtributosValores>(query, new { itemAcervoId, categoriaId, usuarioId }, Transacao);
        }
    }
}
