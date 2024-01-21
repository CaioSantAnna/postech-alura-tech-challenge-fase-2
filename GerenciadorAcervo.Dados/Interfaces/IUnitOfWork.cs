using GerenciadorAcervo.Dados.Repositorios;
using GerenciadorAcervo.Modelos.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Dados.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuariosRepository Usuarios {  get; }
        ICategoriasRepository Categorias { get; }
        ISubCategoriasRepository SubCategorias { get; }
        IAtributosRepository Atributos { get; }
        IItensAcervoAtributosValoresRepository ItensAcervoAtributosValores {  get; }
        IItensAcervoRepository ItensAcervo { get; }
        IContatosRepository Contatos { get; }

        void Commit();
    }
}
