using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.Funcoes
{
    public static class GerenciadorSenha
    {
        public static string GerarHash(string senha, string salt)
        {
            using var sha256 = SHA256.Create();
            string senhaSalt = $"{senha}{salt}";
            byte[] senhaByteArray = Encoding.UTF8.GetBytes(senhaSalt);
            byte[] byteHash = sha256.ComputeHash(senhaByteArray);
            return Convert.ToBase64String(byteHash);
        }

        public static string GerarSalt()
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] byteSalt = new byte[16];
            rng.GetBytes(byteSalt);
            var salt = Convert.ToBase64String(byteSalt);
            return salt;
        }
    }
}
