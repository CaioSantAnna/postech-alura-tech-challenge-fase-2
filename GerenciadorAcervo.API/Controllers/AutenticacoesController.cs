using GerenciadorAcervo.API.Filtros;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.DTOs.Retornos;
using GerenciadorAcervo.Modelos.Funcoes;
using GerenciadorAcervo.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace GerenciadorAcervo.API.Controllers
{
    [ApiController]
    [Authorize]
    public class AutenticacoesController : BaseController
    {
        private IAutenticacoesService _autenticacoesService;
        private readonly ILogger<AutenticacoesController> _logger;

        public AutenticacoesController(IAutenticacoesService autenticacoesService, RetornoApi retornoApi, ILogger<AutenticacoesController> logger) : base(retornoApi)
        {
            _autenticacoesService = autenticacoesService;
            _logger = logger;
        }

        /// <summary>
        /// Permite que usuário cadastrados façam login. Retorna um token JTW com validade de 1 hora e um token JWT com validade de 1 semana para refresh
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]

        [ProducesResponseType(typeof(LoginUsuarioRetorno), StatusCodes.Status200OK)]
        public ActionResult Login(LoginUsuarioRequest request)
        {
            return JsonResponse(_autenticacoesService.Login(request));
        }

        /// <summary>
        /// Permite renovar o token de acesso gerando um novo token JWT com validade de 1 hora e um token JWT com validade de 1 semana para refresh
        /// </summary>
        [HttpGet]
        [Route("refresh-token")]
        [ProducesResponseType(typeof(LoginUsuarioRetorno), StatusCodes.Status200OK)]
        public ActionResult RefreshToken()
        {
            return JsonResponse(_autenticacoesService.RefreshToken());
        }

    }
}
