using GerenciadorAcervo.Modelos.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.DTOs.Requests
{
    public class CadastroAtributoRequest
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [EnumDataType(typeof(AtributoTipoEnum), ErrorMessage = "Tipo de atributo inválido.")]
        public int AtributoTipoId { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public int CategoriaId { get; set; }
        public int? SubCategoriaId { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }
        public string? Descricao { get; set; }
    }
}
