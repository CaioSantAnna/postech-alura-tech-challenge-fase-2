using GerenciadorAcervo.Modelos.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.Tabelas
{
    [Table("ItensAcervoAtributosValores")]
    public class ItensAcervoAtributosValores
    {
        [Key]
        [Column(Order = 0)]
        public int ItemAcervoId { get; set; }
        [Column(Order = 1)]
        public int AtributoId { get; set; }
        public string Valor { get; set; }

        public ItensAcervoAtributosValores()
        {
            
        }

        public ItensAcervoAtributosValores(CadastroItemAcervoAtributoValorRequest objeto, int itemAcervoId)
        {
            ItemAcervoId = itemAcervoId;
            AtributoId = objeto.AtributoId;
            Valor = objeto.Valor;
        }
    }
}