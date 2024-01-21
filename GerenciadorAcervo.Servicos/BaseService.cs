using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Modelos.Funcoes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Servicos
{
    public class BaseService
    {
        private readonly RetornoApi _retornoApi;
        public readonly string emailUsuarioLogado;
        public readonly int usuarioIdUsuarioLogado;
        private ClaimsIdentity _claimsIdentity;
        public BaseService(RetornoApi retorno, IHttpContextAccessor httpContext) {
            _retornoApi = retorno;
            _claimsIdentity = (ClaimsIdentity)httpContext.HttpContext?.User.Identity;
            emailUsuarioLogado = _claimsIdentity?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            usuarioIdUsuarioLogado = Convert.ToInt32(_claimsIdentity?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        }

        protected void AtualizarRetorno(string erro)
            => _retornoApi.AtualizarRetorno(erro);
    }
}
