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
    public class SubCategoriasController : BaseController
    {
        private ISubCategoriasService _subCategoriasService;
        public SubCategoriasController(RetornoApi retorno, ISubCategoriasService subCategoriasService) : base(retorno)
        {
            _subCategoriasService = subCategoriasService;
        }

        /// <summary>
        /// Permite cadastrar subcategorias para categorias existentes
        /// </summary>
        [HttpPost]
        [Route("inserir")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public IActionResult InserirCategoria(CadastroSubCategoriaRequest request)
            => JsonResponse(_subCategoriasService.CadastrarSubCategoria(request));

        /// <summary>
        /// Permite atualizar o cadastro de subcategorias existentes
        /// </summary>
        [HttpPut]
        [Route("atualizar")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult AtualizarCategoria(AlteracaoSubCategoriaRequest request)
            => JsonResponse(_subCategoriasService.AtualizarSubCategoria(request));

        /// <summary>
        /// Permite excluir subcategorias existentes
        /// </summary>
        [HttpDelete]
        [Route("excluir")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult ExcluirCategoria([BindRequired, FromQuery] int categoriaId, [BindRequired, FromQuery] int subCategoriaId)
            => JsonResponse(_subCategoriasService.ExcluirSubCategoria(categoriaId, subCategoriaId));
    }
}
