using GerenciadorAcervo.Modelos.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.DTOs.Retornos
{
    public class ItemAcervoRetorno
    {
        public int ItemAcervoId {  get; set; }
        public string Nome { get; set; }
        public int CategoriaId { get; set; }
        public int? SubCategoriaId { get; set; }
        public int? ContatoId { get; set; }
        public string ContatoNome { get; set; }
        public bool EstahEmprestado { get; set; }

        public List<ItemAcervoAtributoValorRetorno>? AtributosValores { get; set; }
    }
}
