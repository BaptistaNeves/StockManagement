using StockManagement.Application.InputModels.Catalogo;
using StockManagement.Application.ViewModels.Catalogo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockManagement.Application.Interface.Services.Catalogo
{
    public interface ICategoriaService : IDisposable
    {
        Task Adicionar(CategoriaInputModel categoria);
        Task Atualizar(CategoriaInputModel categoria);
        Task Remover(Guid id);
        Task<CategoriaViewModel> ObterPorId(Guid id);
        Task<ICollection<CategoriaViewModel>> ObterTodos();
    }
}
