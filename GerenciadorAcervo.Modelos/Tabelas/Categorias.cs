using GerenciadorAcervo.Modelos.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.Tabelas
{
    [Table("Categorias")]
    public class Categorias
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaId { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        [NotMapped]
        public IEnumerable<SubCategorias> SubCategorias { get; set; }
        [NotMapped]
        [JsonIgnore]
        public int Total { get; set; }

        public Categorias()
        {        
            SubCategorias = new List<SubCategorias>();
        }

        public Categorias(AlteracaoCategoriaRequest objeto, int usuarioId)
        {
            CategoriaId = objeto.CategoriaId;
            UsuarioId = usuarioId;
            Nome = objeto.Nome;
            Descricao = objeto.Descricao;
            Imagem = objeto.Imagem;
        }

        public Categorias(CadastroCategoriaRequest objeto, int usuarioId)
        {
            UsuarioId = usuarioId;
            Nome = objeto.Nome;
            Descricao = objeto.Descricao;
            Imagem = objeto.Imagem;
        }
    }
}
