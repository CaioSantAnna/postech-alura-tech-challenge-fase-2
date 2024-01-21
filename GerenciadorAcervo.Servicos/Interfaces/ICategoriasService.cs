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
    public interface ICategoriasService
    {
        int CadastrarCategoria(CadastroCategoriaRequest objeto);
        bool AtualizarCategoria(AlteracaoCategoriaRequest objeto);
        bool ExcluirCategoria(int categoriaId);
        RetornoPaginado<Categorias> BuscarComFiltros(PaginacaoRequest objeto);
    }
}
