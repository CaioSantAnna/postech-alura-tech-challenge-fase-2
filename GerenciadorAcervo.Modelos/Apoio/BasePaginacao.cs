using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.Apoio
{
    public class BasePaginacao
    {
        public int NumeroPagina { get; set; }
        public int QuantidadeItensPorPagina { get; set; }



        public BasePaginacao() { }
    }
}
