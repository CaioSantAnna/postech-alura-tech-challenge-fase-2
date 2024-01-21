using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Dados.Repositorios
{
    public abstract class BaseRepository
    {
        protected IDbTransaction Transacao { get; set; }
        protected IDbConnection Conexao { get { return Transacao.Connection; } }

        public BaseRepository(IDbTransaction transacao)
        {
            Transacao = transacao;
        }
    }
}
