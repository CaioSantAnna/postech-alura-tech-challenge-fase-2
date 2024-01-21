using GerenciadorAcervo.API.Filtros;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.DTOs.Retornos;
using GerenciadorAcervo.Modelos.Funcoes;
using GerenciadorAcervo.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorAcervo.API.Controllers
{
    [ApiController]
    [Authorize]
    public class UsuariosController : BaseController
    {
        private IUsuariosService _usuariosService;
        
        public UsuariosController(IUsuariosService usuariosService, RetornoApi retornoApi) : base(retornoApi) {
            _usuariosService = usuariosService;            
        }

        /// <summary>
        /// Permite cadastrar novos usuários
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("inserir")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public IActionResult InserirUsuario(CadastroUsuarioRequest request)
             => JsonResponse(_usuariosService.CadastrarUsuario(request));

        /// <summary>
        /// Permite pesquisar usuários por e-mail
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("buscar-por-email")]
        [ProducesResponseType(typeof(UsuarioRetorno), StatusCodes.Status200OK)]
        public IActionResult BuscarUsuario(string email)
             => JsonResponse(_usuariosService.BuscarUsuario(email));

        /// <summary>
        /// Permite pesquisar dados de um usuário logado
        /// </summary>
        [HttpGet]
        [Route("buscar")]
        [ProducesResponseType(typeof(UsuarioRetorno), StatusCodes.Status200OK)]
        public IActionResult BuscarUsuario()
             => JsonResponse(_usuariosService.BuscarUsuario());

        /// <summary>
        /// Permite atualizar dados de um usuário cadastrado
        /// </summary>
        [HttpPut]
        [Route("atualizar")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public IActionResult AtualizarUsuario(AlteracaoUsuarioRequest request)
             => JsonResponse(_usuariosService.Atualizar(request));
    }
}
