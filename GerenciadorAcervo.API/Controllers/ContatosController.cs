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
    public class ContatosController : BaseController
    {
        private IContatosService _contatosService;
        public ContatosController(RetornoApi retorno, IContatosService contatosService) : base(retorno)
        {
            _contatosService = contatosService;
        }

        /// <summary>
        /// Permite cadastrar contatos, pessoas conhecidas para quais será possível registrar que determinado item de acervo foi emprestado
        /// </summary>
        [HttpPost]
        [Route("inserir")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public IActionResult InserirCategoria(CadastroContatoRequest request)
            => JsonResponse(_contatosService.CadastrarContato(request));

        /// <summary>
        /// Permite atualizar contatos, pessoas conhecidas para quais será possível registrar que determinado item de acervo foi emprestado.
        /// </summary>
        [HttpPut]
        [Route("atualizar")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult AtualizarCategoria(AlteracaoContatoRequest request)
            => JsonResponse(_contatosService.AtualizarContato(request));

        /// <summary>
        /// Permite excluir contatos, pessoas conhecidas para quais será possível registrar que determinado item de acervo foi emprestado
        /// </summary>
        [HttpDelete]
        [Route("excluir")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult ExcluirCategoria([BindRequired, FromQuery] int contatoId)
            => JsonResponse(_contatosService.ExcluirContato(contatoId));

        /// <summary>
        /// Permite buscar contatos, pessoas conhecidas para quais será possível registrar que determinado item de acervo foi emprestado
        /// </summary>
        [HttpGet]
        [Route("buscar")]
        [ProducesResponseType(typeof(RetornoPaginado<Contatos>), StatusCodes.Status200OK)]
        public IActionResult BuscarCategorias([FromQuery] PaginacaoRequest request)
         => JsonResponse(_contatosService.BuscarComFiltros(request));
    }
}
