using GerenciadorAcervo.Modelos.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GerenciadorAcervo.Modelos.Tabelas
{
    [Table("ItensAcervo")]
    public class ItensAcervo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemAcervoId { get; set; }
        public int CategoriaId { get; set; }
        public int UsuarioId { get; set; }
        public int? SubCategoriaId { get; set; }
        public string Nome { get; set; }
        public int? ContatoId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public int Total { get; set; }
        public ItensAcervo() { }

        public ItensAcervo(CadastroItemAcervoRequest objeto, int usuarioId)
        {
            SubCategoriaId = objeto.SubCategoriaId;
            CategoriaId = objeto.CategoriaId;
            UsuarioId = usuarioId;
            Nome = objeto.Nome;
            ContatoId = objeto.ContatoId;
        }

        public ItensAcervo(AlteracaoItemAcervoRequest objeto, int usuarioId)
        {
            ItemAcervoId = objeto.ItemAcervoId;
            SubCategoriaId = objeto.SubCategoriaId;
            CategoriaId = Convert.ToInt32(objeto.CategoriaId);
            UsuarioId = usuarioId;
            Nome = objeto.Nome;
            ContatoId = objeto.ContatoId;
        }
    }
}
