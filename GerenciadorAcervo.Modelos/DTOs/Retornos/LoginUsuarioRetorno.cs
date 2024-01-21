using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.DTOs.Retornos
{
    public class LoginUsuarioRetorno
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public long ExpiraEm { get; set; }        
    }
}
