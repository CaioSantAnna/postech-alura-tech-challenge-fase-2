using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.Apoio
{
    public class RetornoApiJson
    {
        public RetornoApiJson(string mensagem, object dados = null)
        {
            Mensagem = mensagem;
            Dados = dados;
        }

        public string Mensagem { get; private set; }
        public object Dados { get; private set; }
    }
}
