using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.DTOs.Requests
{
    public class CadastroItemAcervoAtributoValorRequest
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public int AtributoId { get; set; }
        public string Valor { get; set; }
    }
}
