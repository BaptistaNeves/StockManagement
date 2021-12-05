using StockManagement.Application.InputModels.Pessoa;
using StockManagement.Application.ViewModels.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Application.Interface.Services.Pessoa
{
    public interface IClienteService : IDisposable
    {
        Task Adicionar(ClienteInputModel cliente);
        Task Atualizar(ClienteInputModel cliente);
        Task Remover(Guid id);
        Task<ClienteViewModel> ObterPorId(Guid id);
        Task<ICollection<ClienteViewModel>> ObterTodos();
    }
}
