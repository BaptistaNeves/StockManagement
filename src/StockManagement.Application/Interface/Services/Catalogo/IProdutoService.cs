using StockManagement.Application.InputModels.Catalogo;
using StockManagement.Application.ViewModels.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Application.Interface.Services.Catalogo
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(ProdutoInputModel Produto);
        Task Atualizar(ProdutoInputModel Produto);
        Task Remover(Guid id);
        Task<ProdutoViewModel> ObterPorId(Guid id);
        Task<ICollection<ProdutoViewModel>> ObterTodos();
    }
}
