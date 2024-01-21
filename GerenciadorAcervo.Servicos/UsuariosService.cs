using GerenciadorAcervo.Dados;
using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.DTOs.Retornos;
using GerenciadorAcervo.Modelos.Funcoes;
using GerenciadorAcervo.Modelos.Tabelas;
using GerenciadorAcervo.Servicos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Servicos
{
    public class UsuariosService : BaseService, IUsuariosService
    {
        private IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        private RetornoApi _retornoApi;

        public UsuariosService(IConfiguration configuration, IUnitOfWork unitOfWork, RetornoApi retornoApi, IHttpContextAccessor httpContext) : base(retornoApi, httpContext)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _retornoApi = retornoApi;
        }

        public bool Atualizar(AlteracaoUsuarioRequest request)
        {
            Usuarios dadosConta = _unitOfWork.Usuarios.Buscar(request.Email);
            if (dadosConta != null)
            {
                AtualizarRetorno("Já existe um usuário cadastrado para o e-mail informado. Não é possível atualizar o usuário atual para as informações fornecidas.");
                return false;
            }

            _unitOfWork.Usuarios.Atualizar(new Usuarios(request, usuarioIdUsuarioLogado));
            _unitOfWork.Commit();

            return true;
        }

        public UsuarioRetorno BuscarUsuario(string email)
        {
            Usuarios usuario = _unitOfWork.Usuarios.Buscar(email);
            if(usuario == null)
            {
                AtualizarRetorno("Nenhum usuário encontrado para o e-mail informado.");
                return new UsuarioRetorno();
            }

            return new UsuarioRetorno(usuario);
        }
            

        public UsuarioRetorno BuscarUsuario()
            => new UsuarioRetorno(_unitOfWork.Usuarios.Buscar(usuarioIdUsuarioLogado));

        public int CadastrarUsuario(CadastroUsuarioRequest request)
        {
            Usuarios dadosConta = _unitOfWork.Usuarios.Buscar(request.Email);
            if (dadosConta != null)
            {
                AtualizarRetorno("Já existe um usuário cadastrado para o e-mail informado. Por favor, faça login.");
                return 0;
            }

            int idUsuario = _unitOfWork.Usuarios.Inserir(new Usuarios(request));
            _unitOfWork.Commit();

            return idUsuario;
        }
    }
}
