using GerenciadorAcervo.Dados.Interfaces;
using System.Data;
using GerenciadorAcervo.Modelos.Tabelas;
using Dapper;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using System.Collections.Generic;
using GerenciadorAcervo.Modelos.DTOs.Retornos;

namespace GerenciadorAcervo.Dados.Repositorios
{
    public class UsuariosRepository : BaseRepository, IUsuariosRepository
    {
        public UsuariosRepository(IDbTransaction transacao) : base(transacao)
        {
        }

        public void Atualizar(Usuarios objeto)
        {
            objeto.UsuarioId = Conexao.Execute(@"UPDATE Usuarios
                                                 SET Nome = @Nome,
                                                     Email = @Email
                                                 WHERE UsuarioId = @UsuarioId ", objeto, Transacao);
        }

        public Usuarios Buscar(string email)
        {
            return Conexao.Query<Usuarios>(@"SELECT UsuarioId,
                                                    Nome,
                                                    Email,
                                                    SenhaSalt,
                                                    SenhaHash
                                                  FROM   Usuarios
                                                  WHERE Email = @email", new { email }, Transacao).SingleOrDefault();
        }

        public Usuarios Buscar(int usuarioId)
        {
            return Conexao.Query<Usuarios>(@"SELECT UsuarioId,
                                                    Nome,
                                                    Email,
                                                    SenhaSalt,
                                                    SenhaHash
                                                  FROM   Usuarios
                                                  WHERE UsuarioId = @usuarioId", new { usuarioId }, Transacao).SingleOrDefault();
        }

        public int Inserir(Usuarios objeto)
        {
            objeto.UsuarioId = Conexao.ExecuteScalar<int>(@"INSERT INTO Usuarios
                                                             (Nome,
                                                              Email,
                                                              SenhaSalt,
                                                              SenhaHash)
                                                 VALUES      (@Nome,
                                                              @Email,
                                                              @SenhaSalt,
                                                              @SenhaHash); SELECT SCOPE_IDENTITY() ", objeto, Transacao);

            return objeto.UsuarioId;
        }
    }
}
