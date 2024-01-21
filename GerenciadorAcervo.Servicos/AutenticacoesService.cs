using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.DTOs.Retornos;
using GerenciadorAcervo.Modelos.Tabelas;
using GerenciadorAcervo.Modelos.Funcoes;
using GerenciadorAcervo.Servicos.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace GerenciadorAcervo.Servicos
{
    public class AutenticacoesService : BaseService, IAutenticacoesService
    {
        private IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        private RetornoApi _retornoApi;

        public AutenticacoesService(IConfiguration configuration, IUnitOfWork unitOfWork, RetornoApi retornoApi, IHttpContextAccessor httpContext) : base(retornoApi, httpContext)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _retornoApi = retornoApi;
        }

        public LoginUsuarioRetorno Login(LoginUsuarioRequest request)
        {
            Usuarios dadosConta = _unitOfWork.Usuarios.Buscar(request.Email);
            if (dadosConta == null)
            {
                AtualizarRetorno("Nenhum usuário encontrado para o e-mail informado.");
                return new LoginUsuarioRetorno();
            }

            string senhaComparacao = GerenciadorSenha.GerarHash(request.Senha, dadosConta.SenhaSalt);

            if (senhaComparacao.Equals(dadosConta.SenhaHash))
                return Jwt.GerarToken(dadosConta, _configuration.GetSection("JwtToken:Segredo").Value, _configuration.GetSection("JwtToken:Emissor").Value);
            else
            {
                AtualizarRetorno("Nenhum usuário encontrado para o e-mail informado.");
                return new LoginUsuarioRetorno();
            }
        }

        public LoginUsuarioRetorno RefreshToken()
        {
            Usuarios dadosConta = _unitOfWork.Usuarios.Buscar(base.emailUsuarioLogado);
            if (dadosConta == null)
            {
                AtualizarRetorno("Nenhum usuário encontrado para o e-mail informado.");
                return new LoginUsuarioRetorno();
            }

            return Jwt.GerarToken(dadosConta, _configuration.GetSection("JwtToken:Segredo").Value, _configuration.GetSection("JwtToken:Emissor").Value);
        }
    }
}
