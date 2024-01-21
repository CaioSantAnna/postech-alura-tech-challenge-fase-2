using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.DTOs.Requests
{
    public class PaginacaoItensAcervoRequest
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public int NumeroPagina { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public int QuantidadeItensPorPagina { get; set; }
        public int? CategoriaId { get; set; }
        public int? SubCategoriaId { get; set; }
        public string? TextoPesquisa { get; set;}
    }
}
