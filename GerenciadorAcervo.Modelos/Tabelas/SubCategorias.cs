using GerenciadorAcervo.Modelos.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.Tabelas
{
    public class SubCategorias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubCategoriaId { get; set; }
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }

        public SubCategorias() { }

        public SubCategorias(AlteracaoSubCategoriaRequest objeto)
        {
            SubCategoriaId = objeto.SubCategoriaId;
            CategoriaId = objeto.CategoriaId;
            Nome = objeto.Nome;
            Descricao = objeto.Descricao;
            Imagem = objeto.Imagem;
        }

        public SubCategorias(CadastroSubCategoriaRequest objeto)
        {
            CategoriaId = objeto.CategoriaId;
            Nome = objeto.Nome;
            Descricao = objeto.Descricao;
            Imagem = objeto.Imagem;
        }
    }
}
