using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.DTOs.Retornos
{
    public class UsuarioRetorno
    {
        public string Nome { get; set; }
        public string Email { get; set; }        

        public UsuarioRetorno() { }

        public UsuarioRetorno(Usuarios objeto)
        {
            Nome = objeto.Nome;
            Email = objeto.Email;
        }
    }
}
