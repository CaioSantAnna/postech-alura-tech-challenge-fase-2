using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.DTOs.Requests
{
    public class AlteracaoContatoRequest
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public int ContatoId { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }        
    }
}
