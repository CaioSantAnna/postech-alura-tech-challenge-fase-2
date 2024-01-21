using GerenciadorAcervo.Dados.Interfaces;
using System.Data;
using GerenciadorAcervo.Modelos.Tabelas;
using Dapper;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using System.Collections.Generic;
using GerenciadorAcervo.Modelos.DTOs.Retornos;
using System.Linq;
using System.Text;

namespace GerenciadorAcervo.Dados.Repositorios
{
    public class ContatosRepository : BaseRepository, IContatosRepository
    {
        public ContatosRepository(IDbTransaction transacao) : base(transacao)
        {
        }

        public int Atualizar(Contatos objeto)
        {
            return Conexao.Execute(@"UPDATE Contatos
                                     SET Nome = @Nome
                                     WHERE UsuarioId = @UsuarioId AND ContatoId = @ContatoId ", objeto, Transacao);
        }

        public int Excluir(int contatoId, int usuarioId)
        {
            return Conexao.Execute(@"DELETE FROM Contatos
                                     WHERE UsuarioId = @UsuarioId AND ContatoId = @ContatoId ", new { contatoId, usuarioId }, Transacao);
        }

        public IEnumerable<Contatos> BuscarComFiltros(PaginacaoRequest objeto, int usuarioId)
        {
            int offset = (objeto.NumeroPagina - 1) * objeto.QuantidadeItensPorPagina;
            StringBuilder where = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(objeto.TextoPesquisa))
                where.Append($" AND Nome LIKE CONCAT('%',@TextoPesquisa,'%')");

            var query = @"SELECT *, Count(*) OVER () AS Total
                          FROM   Contatos
                          WHERE UsuarioId = @usuarioId [whereComplemento]
                          ORDER  BY Nome
                          OFFSET @offset ROWS FETCH NEXT @QuantidadeItensPorPagina ROWS ONLY";

            return Conexao.Query<Contatos>(query.Replace("[whereComplemento]", where.ToString()), new { offset, objeto.QuantidadeItensPorPagina, usuarioId, objeto.TextoPesquisa }, Transacao);
        }

        public IEnumerable<Contatos> BuscarTodos(int usuarioId)
        {
            
            var query = @"SELECT *
                          FROM   Contatos
                          WHERE UsuarioId = @usuarioId";

            return Conexao.Query<Contatos>(query, new { usuarioId }, Transacao);
        }

        public bool EhContatoValido(int contatoId, int usuarioId)
        {
            var query = @"IF EXISTS (SELECT *
                                     FROM   Contatos
                                     WHERE  UsuarioId = @UsuarioId
                                            AND ContatoId = @ContatoId)
                            BEGIN
                                SELECT 1
                            END
                          ELSE
                            BEGIN
                                SELECT 0
                            END";

            return Conexao.Query<bool>(query, new { usuarioId, contatoId }, Transacao).SingleOrDefault();
        }

        public int Inserir(Contatos objeto)
        {
            objeto.ContatoId = Conexao.ExecuteScalar<int>(@"INSERT INTO Contatos
                                                                        (Nome,
                                                                         UsuarioId)
                                                            VALUES      (@Nome,
                                                                         @UsuarioId) ; SELECT SCOPE_IDENTITY() ", objeto, Transacao);

            return objeto.ContatoId;
        }
    }
}
