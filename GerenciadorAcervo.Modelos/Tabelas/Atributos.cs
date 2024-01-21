using GerenciadorAcervo.Modelos.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GerenciadorAcervo.Modelos.Tabelas
{
    [Table("Atributos")]
    public class Atributos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AtributoId { get; set; }
        public int AtributoTipoId { get; set; }
        public int CategoriaId { get; set; }
        public int? SubCategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public Atributos() { }

        public Atributos(AlteracaoAtributoRequest objeto)
        {
            AtributoId = objeto.AtributoId;
            CategoriaId = objeto.CategoriaId;
            SubCategoriaId = objeto.SubCategoriaId;
            Nome = objeto.Nome;
            Descricao = objeto.Descricao;
        }

        public Atributos(CadastroAtributoRequest objeto)
        {
            AtributoTipoId = objeto.AtributoTipoId;
            CategoriaId = objeto.CategoriaId;
            SubCategoriaId = objeto.SubCategoriaId;
            Nome = objeto.Nome;
            Descricao = objeto.Descricao;
        }
    }
}
