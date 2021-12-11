using Microsoft.EntityFrameworkCore;
using StockManagement.Core.DTOs.Catalogo;
using StockManagement.Core.Entities.Catalogo;
using StockManagement.Core.Interfaces.Persistence.Repositories.Catalogo;
using StockManagement.Infrastructure.Persistence.Context;
using StockManagement.Infrastructure.Persistence.Repositories.Generico;
using StockManagement.Shared.Pagination;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Repositories.Catalogo
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context){}

        public async Task<PagedList<ProdutoDto>> ObterProdutos(PaginationParams paginationParams)
        {
            var query = _context.Produtos.AsNoTracking()
                                .Include(p => p.Categoria)
                                .Select(produto => new ProdutoDto
                                {
                                    Id = produto.Id,
                                    Nome = produto.Nome,
                                    Preco = (double) produto.Preco,
                                    Categoria = produto.Categoria.Descricao,
                                    Imagem = produto.Imagem,
                                    Descricao = produto.Descricao,
                                    Estocavel = (bool) produto.Estocavel,
                                    EstoqueMinimo = (int) produto.EstoqueMinimo,
                                    DataCadastro = produto.DataCadastro
                                });

            return await PagedList<ProdutoDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<Produto> ObterProdutoVendasEstoquePorId(Guid id)
        {
            return await _context.Produtos.AsNoTracking()
                                 .Include(p => p.Vendas)
                                 .Include(p => p.Estoque)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
