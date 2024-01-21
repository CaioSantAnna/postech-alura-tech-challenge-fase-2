using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.DTOs.Retornos;
using GerenciadorAcervo.Modelos.Tabelas;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.Funcoes
{
    public static class Jwt
    {
        public static LoginUsuarioRetorno GerarToken(Usuarios dadosContaLogin, string segredo, string emissor)
        {
            DateTime dataExpiracaoToken = DateTime.UtcNow.AddMinutes(60);
            DateTime dataExpiracaoRefreshToken = DateTime.UtcNow.AddMinutes(10080);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, dadosContaLogin.Email));
            claims.Add(new Claim(ClaimTypes.Name, dadosContaLogin.Nome));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.Now).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.Now).ToString(), ClaimValueTypes.Integer64));
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(segredo);

            var refreshToken = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = emissor,
                Subject = new ClaimsIdentity(claims),
                Expires = dataExpiracaoRefreshToken,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
            });

            claims.Add(new Claim(ClaimTypes.NameIdentifier, dadosContaLogin.UsuarioId.ToString()));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                NotBefore = DateTime.Now,
                Issuer = emissor,
                Subject = identityClaims,
                Expires = dataExpiracaoToken,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
            });

            var tokenCodificado = tokenHandler.WriteToken(token);
            var refreshTokenCodificado = tokenHandler.WriteToken(refreshToken);

            var loginUsuario = new LoginUsuarioRetorno
            {
                Token = tokenCodificado,
                RefreshToken = refreshTokenCodificado,
                ExpiraEm = ToUnixEpochDate(dataExpiracaoToken),                
            };
            return loginUsuario;
        }

        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
