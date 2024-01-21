using GerenciadorAcervo.Modelos.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.Tabelas
{
    [Table("Usuarios")]
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }        
        public string Nome { get; set; }        
        public string Email { get; set; }
        public string SenhaSalt { get; set; }
        public string SenhaHash { get; set; }

        public Usuarios()
        {
            
        }

        public Usuarios(CadastroUsuarioRequest objeto) {
            Nome = objeto.Nome;
            Email = objeto.Email;
            SenhaSalt = Funcoes.GerenciadorSenha.GerarSalt();
            SenhaHash = Funcoes.GerenciadorSenha.GerarHash(objeto.Senha, SenhaSalt);
        }

        public Usuarios(AlteracaoUsuarioRequest objeto, int usuarioId)
        {
            UsuarioId = usuarioId;
            Nome = objeto.Nome;
            Email = objeto.Email;            
        }
    }
}
