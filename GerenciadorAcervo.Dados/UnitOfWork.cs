using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Dados.Repositorios;
using GerenciadorAcervo.Modelos.Tabelas;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace GerenciadorAcervo.Dados
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _conexao;
        private IDbTransaction _transacao;
        private IConfiguration _configuracao;
        public IUsuariosRepository Usuarios { get; set; }
        public ICategoriasRepository Categorias { get; set; }
        public ISubCategoriasRepository SubCategorias { get; set; }
        public IAtributosRepository Atributos { get; set; }
        public IItensAcervoAtributosValoresRepository ItensAcervoAtributosValores { get; }
        public IItensAcervoRepository ItensAcervo { get; }
        public IContatosRepository Contatos { get; }

        public UnitOfWork(IConfiguration configuracao)
        {
            _configuracao = configuracao;
            _conexao = new SqlConnection(_configuracao.GetConnectionString("StringConexao"));
            _conexao.Open();
            _transacao = _conexao.BeginTransaction();
            Usuarios = new UsuariosRepository(_transacao);
            Categorias = new CategoriasRepository(_transacao);
            SubCategorias = new SubCategoriasRepository(_transacao);
            Atributos = new AtributosRepository(_transacao);
            ItensAcervoAtributosValores = new ItensAcervoAtributosValoresRepository(_transacao);
            ItensAcervo = new ItensAcervoRepository(_transacao);
            Contatos = new ContatosRepository(_transacao);
        }

        public void Commit()
        {
            try
            {
                _transacao.Commit();
            }
            catch
            {
                _transacao.Rollback();
                throw;
            }
            finally
            {
                _transacao.Dispose();
                _transacao = _conexao.BeginTransaction();             
            }
        }

        public void Dispose()
        {
            if (_transacao != null)            
                _transacao.Dispose();

            if (_conexao != null)
                _conexao.Dispose();            
        }
    }
}
