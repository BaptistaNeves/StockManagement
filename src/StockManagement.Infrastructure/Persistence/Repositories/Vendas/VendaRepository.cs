using Microsoft.EntityFrameworkCore;
using StockManagement.Core.DTOs.Vendas;
using StockManagement.Core.Entities.Vendas;
using StockManagement.Core.Interfaces.Persistence.Repositories.Vendas;
using StockManagement.Infrastructure.Persistence.Context;
using StockManagement.Infrastructure.Persistence.Repositories.Generico;
using StockManagement.Shared.Pagination;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Repositories.Vendas
{
    public class VendaRepository : Repository<Venda>, IVendaRepository
    {
        public VendaRepository(AppDbContext context) : base(context) {}

        public async Task<VendaDto> ObterVendaPorId(Guid id)
        {
            return await _context.Vendas.AsNoTracking()
                            .Include(v => v.Cliente)
                            .Include(v => v.VendaProdutos)
                            .ThenInclude(vp => vp.Produto)
                            .Select(venda => new VendaDto
                            {
                                Id = venda.Id,
                                NomeDoCliente = venda.Cliente.Nome,
                                Total = (double)venda.Total,
                                DataDaVenda = venda.DataCadastro,
                                Observacao = venda.Observacao,
                                NomeDoProduto = venda.VendaProdutos.Select(vp => vp.Produto.Nome).ToString(),
                                Quantidade = (int)venda.VendaProdutos.Select(vp => vp.Quantidade).FirstOrDefault(),
                                PrecoUnitario = (double)venda.VendaProdutos.Select(vp => vp.PrecoUnitario).FirstOrDefault(),
                                SubTotal = (double)venda.VendaProdutos.Select(vp => vp.Subtotal).FirstOrDefault()
                            }).FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<PagedList<VendaDto>> ObterVendas(PaginationParams paginationParams)
        {
            var query = _context.Vendas.AsNoTracking()
                            .Include(v => v.Cliente)
                            .Include(v => v.VendaProdutos)
                            .ThenInclude(vp => vp.Produto)
                            .Select(venda => new VendaDto
                            {
                                Id = venda.Id,
                                NomeDoCliente = venda.Cliente.Nome,
                                Total = (double)venda.Total,
                                DataDaVenda = venda.DataCadastro,
                                Observacao = venda.Observacao,
                                NomeDoProduto = venda.VendaProdutos.Select(vp => vp.Produto.Nome).ToString(),
                                Quantidade = (int) venda.VendaProdutos.Select(vp => vp.Quantidade).FirstOrDefault(),
                                PrecoUnitario = (double) venda.VendaProdutos.Select(vp => vp.PrecoUnitario).FirstOrDefault(),
                                SubTotal = (double)venda.VendaProdutos.Select(vp => vp.Subtotal).FirstOrDefault()
                            });

            return await PagedList<VendaDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public Task<PagedList<VendaDto>> ObterVendasDeHoje(PaginationParams paginationParams)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<VendaDto>> ObterVendasPorMes(string mes, PaginationParams paginationParams)
        {
            throw new NotImplementedException();
        }
    }
}
