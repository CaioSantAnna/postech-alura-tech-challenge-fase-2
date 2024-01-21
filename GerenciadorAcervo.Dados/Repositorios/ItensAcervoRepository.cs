using Dapper;
using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Dados.Repositorios
{
    public class ItensAcervoRepository : BaseRepository, IItensAcervoRepository
    {
        public ItensAcervoRepository(IDbTransaction transacao) : base(transacao)
        {
        }

        public int Atualizar(ItensAcervo objeto)
        {
            return Conexao.Execute(@"UPDATE ItensAcervo SET Nome = @Nome, ContatoId = @ContatoId WHERE ItemAcervoId = @ItemAcervoId AND UsuarioId = @UsuarioId", objeto, Transacao);
        }

        public int Excluir(int itemAcervoId, int usuarioId)
        {
            return Conexao.Execute(@"DELETE FROM ItensAcervo 
                              WHERE ItemAcervoId = @itemAcervoId AND UsuarioId = @usuarioId", new { itemAcervoId, usuarioId }, Transacao);
        }

        public int Inserir(ItensAcervo objeto)
        {
            objeto.ItemAcervoId = Conexao.ExecuteScalar<int>(@"INSERT INTO ItensAcervo
                                                               (CategoriaId,
                                                                SubCategoriaId,
                                                                Nome,
                                                                UsuarioId,
                                                                ContatoId)
                                                   VALUES      (@CategoriaId,
                                                                @SubCategoriaId,
                                                                @Nome,
                                                                @UsuarioId,
                                                                @ContatoId); SELECT SCOPE_IDENTITY() ", objeto, Transacao);

            return objeto.ItemAcervoId;
        }

        public ItensAcervo Buscar(ItensAcervo objeto)
        {
            var query = @"SELECT ItemAcervoId,
                                 CategoriaId,
                                 SubCategoriaId,
                                 Nome,
                                 UsuarioId,
                                 ContatoId
                          FROM   ItensAcervo
                          WHERE  ItemAcervoId = @ItemAcervoId
                                    AND UsuarioId = @UsuarioId
                                    AND CategoriaId = @CategoriaId" +
                                    $" AND SubCategoriaId {(objeto.SubCategoriaId == null ? "IS NULL" : "= @SubCategoriaId")}";

            return Conexao.Query<ItensAcervo>(query, objeto, Transacao).SingleOrDefault();
        }

        public IEnumerable<ItensAcervo> BuscarComFiltros(PaginacaoItensAcervoRequest objeto, int usuarioId)
        {
            int offset = (objeto.NumeroPagina - 1) * objeto.QuantidadeItensPorPagina;

            StringBuilder where = new StringBuilder();

            if(!string.IsNullOrWhiteSpace(objeto.TextoPesquisa))
                where.Append($" AND Nome LIKE CONCAT('%',@TextoPesquisa,'%')");

            if (objeto.CategoriaId != null)
                where.Append($" AND CategoriaId = @CategoriaId");

            if (objeto.SubCategoriaId != null)
                where.Append($" AND SubCategoriaId = @SubCategoriaId");

            var query = @"SELECT ItemAcervoId,
                                 CategoriaId,
                                 SubCategoriaId,
                                 Nome,
                                 UsuarioId,
                                 ContatoId,
                                 Count(*) OVER () AS Total
                          FROM   ItensAcervo
                          WHERE  UsuarioId = @usuarioId [whereComplemento]
                          ORDER  BY Nome 
                          OFFSET @offset ROWS FETCH NEXT @QuantidadeItensPorPagina ROWS ONLY";

            return Conexao.Query<ItensAcervo>(query.Replace("[whereComplemento]", where.ToString()), new { offset, objeto.QuantidadeItensPorPagina, usuarioId, objeto.TextoPesquisa, objeto.CategoriaId, objeto.SubCategoriaId }, Transacao);
        }
    }
}
