using GerenciadorAcervo.Modelos.Apoio;
using GerenciadorAcervo.Modelos.Funcoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GerenciadorAcervo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly RetornoApi _retorno;
        protected BaseController(RetornoApi retorno)
        {
            _retorno = retorno;        
        }
        protected bool ResponseValido()
        {
            return !_retorno.TemErro();
        }
        protected ActionResult JsonResponse(object retorno = null)
        {
            if (ResponseValido())
            {
                return Ok(retorno);
            }

            return BadRequest(_retorno.BuscarErro());
        }
    }
}
