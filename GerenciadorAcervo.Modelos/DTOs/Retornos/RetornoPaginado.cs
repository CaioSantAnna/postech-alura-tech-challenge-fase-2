using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.DTOs.Retornos
{
    public class RetornoPaginado<T>
    {
        public int ItensTotal { get; set; }
        public List<T> Itens { get; set; }
    }
}
