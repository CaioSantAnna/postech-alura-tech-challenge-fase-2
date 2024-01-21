using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.DTOs.Retornos;
using GerenciadorAcervo.Modelos.Funcoes;
using GerenciadorAcervo.Servicos;
using GerenciadorAcervo.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GerenciadorAcervo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItensAcervoController : BaseController
    {
        private IItensAcervoService _itensAcervoService;
        public ItensAcervoController(RetornoApi retorno, IItensAcervoService itensAcervoService) : base(retorno)
        {
            _itensAcervoService = itensAcervoService;
        }

        /// <summary>
        /// Permite cadastrar novos itens para inclusão no acervo
        /// </summary>
        [HttpPost]
        [Route("inserir")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public IActionResult InserirItemAcervo(CadastroItemAcervoRequest request)
            => JsonResponse(_itensAcervoService.InserirItemAcervo(request));

        /// <summary>
        /// Permite buscar itens cadastrados por nome, categoria e subcategoria
        /// </summary>
        [HttpGet]
        [Route("buscar")]
        [ProducesResponseType(typeof(RetornoPaginado<ItemAcervoRetorno>), StatusCodes.Status200OK)]
        public IActionResult BuscarItensAcervo([FromQuery] PaginacaoItensAcervoRequest request)
            => JsonResponse(_itensAcervoService.BuscarComFiltros(request));

        /// <summary>
        /// Permite excluir itens cadastrados
        /// </summary>
        [HttpDelete]
        [Route("excluir")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public IActionResult ExcluirItemAcervo([BindRequired, FromQuery] int itemAcervoId)
            => JsonResponse(_itensAcervoService.ExcluirItemAcervo(itemAcervoId));

        /// <summary>
        /// Permite atualizar o cadastro de itens existentes
        /// </summary>
        [HttpPut]
        [Route("atualizar")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult AtualizarItemAcervo(AlteracaoItemAcervoRequest request)
            => JsonResponse(_itensAcervoService.AtualizarItemAcervo(request));
    }
}
