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

        public async Task AnularVenda(Guid vendaId)
        {
            var venda = await _context.Vendas.AsNoTracking()
                                .FirstOrDefaultAsync(e => e.Id == vendaId);
            venda.AnularVenda();

            _context.Update(venda);
        }

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
                                Total = venda.Total,
                                DataDaVenda = venda.DataCadastro,
                                Observacao = venda.Observacao,
                                ProdutoId = venda.VendaProdutos.Select(vp => vp.ProdutoId).FirstOrDefault(),
                                NomeDoProduto = venda.VendaProdutos.Select(vp => vp.Produto.Nome).ToString(),
                                Quantidade = venda.VendaProdutos.Select(vp => vp.Quantidade).FirstOrDefault(),
                                PrecoUnitario = venda.VendaProdutos.Select(vp => vp.PrecoUnitario).FirstOrDefault(),
                                SubTotal = venda.VendaProdutos.Select(vp => vp.Subtotal).FirstOrDefault(),
                                Status = venda.Status == false ? "Anulada" : "Activa"
                            }).FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<PagedList<VendaDto>> ObterVendas(PaginationParams paginationParams)
        {
            var query = _context.Vendas.AsNoTracking()
                            .Include(v => v.Cliente)
                            .Include(v => v.VendaProdutos)
                            .ThenInclude(vp => vp.Produto)
                            .Where(v => v.Status != false)
                            .Select(venda => new VendaDto
                            {
                                Id = venda.Id,
                                NomeDoCliente = venda.Cliente.Nome,
                                Total = venda.Total,
                                DataDaVenda = venda.DataCadastro,
                                Observacao = venda.Observacao,
                                ProdutoId = venda.VendaProdutos.Select(vp => vp.ProdutoId).FirstOrDefault(),
                                NomeDoProduto = venda.VendaProdutos.Select(vp => vp.Produto.Nome).FirstOrDefault(),
                                Quantidade = venda.VendaProdutos.Select(vp => vp.Quantidade).FirstOrDefault(),
                                PrecoUnitario = venda.VendaProdutos.Select(vp => vp.PrecoUnitario).FirstOrDefault(),
                                SubTotal = venda.VendaProdutos.Select(vp => vp.Subtotal).FirstOrDefault(),
                                Status = venda.Status == false ? "Anulada" : "Activa"
                            });

            return await PagedList<VendaDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<PagedList<VendaDto>> ObterVendasAnuladas(PaginationParams paginationParams)
        {
            var query = _context.Vendas.AsNoTracking()
                           .Include(v => v.Cliente)
                           .Include(v => v.VendaProdutos)
                           .ThenInclude(vp => vp.Produto)
                           .Where(v => v.Status == false)
                           .Select(venda => new VendaDto
                           {
                               Id = venda.Id,
                               NomeDoCliente = venda.Cliente.Nome,
                               Total = venda.Total,
                               DataDaVenda = venda.DataCadastro,
                               Observacao = venda.Observacao,
                               ProdutoId = venda.VendaProdutos.Select(vp => vp.ProdutoId).FirstOrDefault(),
                               NomeDoProduto = venda.VendaProdutos.Select(vp => vp.Produto.Nome).FirstOrDefault(),
                               Quantidade = venda.VendaProdutos.Select(vp => vp.Quantidade).FirstOrDefault(),
                               PrecoUnitario = venda.VendaProdutos.Select(vp => vp.PrecoUnitario).FirstOrDefault(),
                               SubTotal = venda.VendaProdutos.Select(vp => vp.Subtotal).FirstOrDefault(),
                               Status = venda.Status == false ? "Anulada" : "Activa"
                           });

            return await PagedList<VendaDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<PagedList<VendaDto>> ObterVendasDeHoje(PaginationParams paginationParams)
        {
            var hoje = DateTime.Now.Date;

            var query = _context.Vendas.AsNoTracking()
                        .Include(v => v.Cliente)
                        .Include(v => v.VendaProdutos)
                        .ThenInclude(vp => vp.Produto)
                        .Where(v => v.DataCadastro.Date == hoje)
                        .Select(venda => new VendaDto
                        {
                            Id = venda.Id,
                            NomeDoCliente = venda.Cliente.Nome,
                            Total = (double)venda.Total,
                            DataDaVenda = venda.DataCadastro,
                            Observacao = venda.Observacao,
                            ProdutoId = venda.VendaProdutos.Select(vp => vp.ProdutoId).FirstOrDefault(),
                            NomeDoProduto = venda.VendaProdutos.Select(vp => vp.Produto.Nome).FirstOrDefault(),
                            Quantidade = (int)venda.VendaProdutos.Select(vp => vp.Quantidade).FirstOrDefault(),
                            PrecoUnitario = (double)venda.VendaProdutos.Select(vp => vp.PrecoUnitario).FirstOrDefault(),
                            SubTotal = (double)venda.VendaProdutos.Select(vp => vp.Subtotal).FirstOrDefault(),
                            Status = venda.Status == false ? "Anulada" : "Activa"
                        });

            return await PagedList<VendaDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);

        }

        public async Task<PagedList<VendaDto>> ObterVendasPorMes(int mes, PaginationParams paginationParams)
        {
            var query = _context.Vendas.AsNoTracking()
                        .Include(v => v.Cliente)
                        .Include(v => v.VendaProdutos)
                        .ThenInclude(vp => vp.Produto)
                        .Where(v => v.DataCadastro.Month == mes)
                        .Select(venda => new VendaDto
                        {
                            Id = venda.Id,
                            NomeDoCliente = venda.Cliente.Nome,
                            Total = (double)venda.Total,
                            DataDaVenda = venda.DataCadastro,
                            Observacao = venda.Observacao,
                            ProdutoId = venda.VendaProdutos.Select(vp => vp.ProdutoId).FirstOrDefault(),
                            NomeDoProduto = venda.VendaProdutos.Select(vp => vp.Produto.Nome).FirstOrDefault(),
                            Quantidade = (int)venda.VendaProdutos.Select(vp => vp.Quantidade).FirstOrDefault(),
                            PrecoUnitario = (double)venda.VendaProdutos.Select(vp => vp.PrecoUnitario).FirstOrDefault(),
                            SubTotal = (double)venda.VendaProdutos.Select(vp => vp.Subtotal).FirstOrDefault(),
                            Status = venda.Status == false ? "Anulada" : "Activa"
                        });

            return await PagedList<VendaDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}
