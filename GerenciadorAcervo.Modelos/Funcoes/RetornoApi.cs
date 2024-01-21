using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.Funcoes
{
    public class RetornoApi
    {
        private string _erro { get; set; }

        public RetornoApi()
        {
            _erro = String.Empty;            
        }

        public void AtualizarRetorno(string erro = null)
        {
            _erro = erro;
        }

        public string BuscarErro() => _erro;

        public bool TemErro() => !string.IsNullOrWhiteSpace(_erro);
    }
}
