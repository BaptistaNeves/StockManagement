using StockManagement.Application.InputModels.Pessoa;
using StockManagement.Core.DTOs.Pessoa;
using StockManagement.Shared.Pagination;
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
        Task<ClienteDto> ObterPorId(Guid id);
        Task<PagedList<ClienteDto>> ObterTodos(PaginationParams paginationParams);
    }
}
