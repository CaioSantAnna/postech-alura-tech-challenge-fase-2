using GerenciadorAcervo.Modelos.DTOs.Requests;
using GerenciadorAcervo.Modelos.DTOs.Retornos;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Servicos.Interfaces
{
    public interface IUsuariosService
    {
        int CadastrarUsuario(CadastroUsuarioRequest objeto);
        UsuarioRetorno BuscarUsuario(string email);
        UsuarioRetorno BuscarUsuario();
        bool Atualizar(AlteracaoUsuarioRequest objeto);
    }
}
