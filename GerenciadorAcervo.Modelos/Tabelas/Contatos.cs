using GerenciadorAcervo.Modelos.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GerenciadorAcervo.Modelos.Tabelas
{
    [Table("Contatos")]
    public class Contatos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContatoId { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        [NotMapped]
        [JsonIgnore]
        public int Total { get; set; }

        public Contatos()
        {
            
        }

        public Contatos(CadastroContatoRequest objeto, int usuarioId)
        {
            UsuarioId = usuarioId;
            Nome = objeto.Nome;
        }

        public Contatos(AlteracaoContatoRequest objeto, int usuarioId)
        {
            ContatoId = objeto.ContatoId;
            UsuarioId = usuarioId;
            Nome = objeto.Nome;
        }
    }
}
