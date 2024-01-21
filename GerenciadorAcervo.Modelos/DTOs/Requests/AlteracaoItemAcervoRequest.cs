using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.DTOs.Requests
{
    public class AlteracaoItemAcervoRequest
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public int ItemAcervoId { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public int CategoriaId { get; set; }
        public int? SubCategoriaId { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }
        public int? ContatoId { get; set; }
        public List<CadastroItemAcervoAtributoValorRequest>? AtributosValores { get; set; }

        public AlteracaoItemAcervoRequest()
        {
            AtributosValores = new List<CadastroItemAcervoAtributoValorRequest>();
        }
    }
}
