using Azure.Core;
using GerenciadorAcervo.Modelos.Apoio;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.DTOs.Retornos;
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
    public class CategoriasController : BaseController
    {
        private ICategoriasService _categoriasService;
        public CategoriasController(RetornoApi retorno, ICategoriasService categoriasService) : base(retorno)
        {
            _categoriasService = categoriasService;
        }

        /// <summary>
        /// Permite cadastrar categorias novas para estruturar itens de acervo que serão cadastrados posteriormente da maneira mais conveniente possível
        /// </summary>
        [HttpPost]
        [Route("inserir")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public IActionResult InserirCategoria(CadastroCategoriaRequest request)
            => JsonResponse(_categoriasService.CadastrarCategoria(request));

        /// <summary>
        /// Permite atualuzar categorias existentes para estruturar itens de acervo que serão cadastrados posteriormente da maneira mais conveniente possível
        /// </summary>
        [HttpPut]
        [Route("atualizar")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult AtualizarCategoria(AlteracaoCategoriaRequest request)
            => JsonResponse(_categoriasService.AtualizarCategoria(request));

        /// <summary>
        /// Permite excluir categorias existentes para estruturar itens de acervo que serão cadastrados posteriormente da maneira mais conveniente possível
        /// </summary>

        [HttpDelete]
        [Route("excluir")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult ExcluirCategoria([BindRequired, FromQuery] int categoriaId)
            => JsonResponse(_categoriasService.ExcluirCategoria(categoriaId));

        /// <summary>
        /// Permite buscar as categorias cadastradas no momento. O retorno inclui a lista de subcategorias existente para cada categoria retornada.
        /// </summary>
        [HttpGet]
        [Route("buscar")]
        [ProducesResponseType(typeof(RetornoPaginado<Categorias>), StatusCodes.Status200OK)]
        public IActionResult BuscarCategorias([FromQuery] PaginacaoRequest request)
         => JsonResponse(_categoriasService.BuscarComFiltros(request));
    }
}
