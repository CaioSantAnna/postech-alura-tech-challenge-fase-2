using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GerenciadorAcervo.API.Configuracoes
{
    public static class JwtConfiguracao
    {
        public static IServiceCollection AdicionarJwtConfiguracao(this IServiceCollection services, IConfiguration configuration)
        {
            string chave = configuration.GetValue<string>("JwtToken:Segredo");
            string emissor = configuration.GetValue<string>("JwtToken:Emissor");

            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave)),
                    ValidateIssuer = true,
                    ValidIssuer = emissor,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }
    }
}
