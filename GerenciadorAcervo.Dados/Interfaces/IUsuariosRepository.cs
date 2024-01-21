using GerenciadorAcervo.Modelos.DTOs.Retornos;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Dados.Interfaces
{
    public interface IUsuariosRepository
    {
        Usuarios Buscar(string email);
        Usuarios Buscar(int usuarioId);
        int Inserir(Usuarios objeto);
        void Atualizar(Usuarios objeto);
    }
}
