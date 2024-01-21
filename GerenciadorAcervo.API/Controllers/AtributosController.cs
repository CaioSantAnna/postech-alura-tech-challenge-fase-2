using GerenciadorAcervo.Modelos.Dtos;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.Enums;
using GerenciadorAcervo.Modelos.Funcoes;
using GerenciadorAcervo.Modelos.Tabelas;
using GerenciadorAcervo.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GerenciadorAcervo.API.Controllers
{
    [ApiController]
    [Authorize]
    public class AtributosController : BaseController
    {
        private IAtributosService _atributosService;

        public AtributosController(RetornoApi retorno, IAtributosService atributosService) : base(retorno)
        {
            _atributosService = atributosService;
        }

        /// <summary>
        /// Retorna lista de tipos de atributos disponíveis para criação de atributos
        /// </summary>
        [HttpGet("atributo-tipos")]
        [ProducesResponseType(typeof(List<EnumDTO>), StatusCodes.Status200OK)]
        public ActionResult BuscarAtributosTipos()
        {
            return JsonResponse(Enum<AtributoTipoEnum>.BuscarValoresComoIEnumerable().Select(x => new EnumDTO(x)));
        }

        /// <summary>
        /// Retorna lista de atributos cadastrados por categoria
        /// </summary>
        [HttpGet("buscar-por-categoria")]
        [ProducesResponseType(typeof(List<Atributos>), StatusCodes.Status200OK)]
        public ActionResult BuscarTodosPorCategoria([BindRequired, FromQuery] int categoriaId)
            => JsonResponse(_atributosService.BuscarTodosPorCategoria(categoriaId));

        /// <summary>
        /// Retorna lista de atributos cadastrados por categoria e subcategoria
        /// </summary>
        [HttpGet("buscar-por-subcategoria")]
        [ProducesResponseType(typeof(List<Atributos>), StatusCodes.Status200OK)]
        public ActionResult BuscarTodosPorSubCategoria([BindRequired, FromQuery] int categoriaId, [BindRequired, FromQuery] int subCategoriaId)
            => JsonResponse(_atributosService.BuscarTodosPorSubCategoria(categoriaId, subCategoriaId));

        /// <summary>
        /// Permite cadastrar novos atributos para uma determinada categoria/subcategoria
        /// </summary>
        [HttpPost]
        [Route("inserir")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public IActionResult CadastrarAtributo(CadastroAtributoRequest request)
            => JsonResponse(_atributosService.CadastrarAtributo(request));

        /// <summary>
        /// Permite atualizar atributos para uma determinada categoria/subcategoria
        /// </summary>
        [HttpPut]
        [Route("atualizar")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult AtualizarAtributo(AlteracaoAtributoRequest request)
            => JsonResponse(_atributosService.AtualizarAtributo(request));

        /// <summary>
        /// Permite excluir atributos para uma determinada categoria/subcategoria
        /// </summary>
        [HttpDelete]
        [Route("excluir")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult ExcluirAtributo([BindRequired, FromQuery] int categoriaId, [FromQuery] int? subCategoriaId, [BindRequired, FromQuery] int atributoId)
            => JsonResponse(_atributosService.ExcluirAtributo(categoriaId, subCategoriaId, atributoId));
    }
}
