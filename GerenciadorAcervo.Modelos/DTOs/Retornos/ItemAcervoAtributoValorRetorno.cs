using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.DTOs.Retornos
{
    public class ItemAcervoAtributoValorRetorno
    {
        public int AtributoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Valor { get; set; }
        public int AtributoTipoId { get; set; }
    }
}
