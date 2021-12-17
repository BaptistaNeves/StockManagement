using StockManagement.Application.InputModels.Vendas;
using StockManagement.Core.DTOs.Vendas;
using StockManagement.Core.Entities.Vendas;
using StockManagement.Shared.Pagination;
using System;
using System.Threading.Tasks;

namespace StockManagement.Application.Interface.Services.Vendas
{
    public interface IVendaService : IDisposable
    {
        Task Adicionar(VendaInputModel vendaModel);
        //Task Atualizar(VendaInputModel vendaModel);
        Task Remover(Guid id);
        Task<Venda> ConsultarVendaPorId(Guid id);
        Task<VendaDto> ObterVendaPorId(Guid id);
        Task<PagedList<VendaDto>> ObterVendas(PaginationParams paginationParams);
        Task<PagedList<VendaDto>> ObterVendasAnuladas(PaginationParams paginationParams);
        Task<PagedList<VendaDto>> ObterVendasDeHoje(PaginationParams paginationParams);
        Task<PagedList<VendaDto>> ObterVendasPorMes(int mes, PaginationParams paginationParams);
        Task AnularVenda(Guid vendaId, Guid produtoId, int quantidadeProduto);
    }
}
