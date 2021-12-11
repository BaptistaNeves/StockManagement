using Microsoft.EntityFrameworkCore;
using StockManagement.Core.DTOs.Movimentacao;
using StockManagement.Core.Entities.Movimentacao;
using StockManagement.Core.Interfaces.Persistence.Repositories.Movimentacao;
using StockManagement.Infrastructure.Persistence.Context;
using StockManagement.Infrastructure.Persistence.Repositories.Generico;
using StockManagement.Shared.Pagination;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Repositories.Movimentacao
{
    public class EstoqueRepository : Repository<Estoque>, IEstoqueRepository
    {
        public EstoqueRepository(AppDbContext context) : base(context){}

        public async Task DecrementarQuantidadeProdutoNoEstoque(Guid produtoId, int quantidade)
        {
            var estoque = await _context.Estoques.AsNoTracking()
                                .FirstOrDefaultAsync(e => e.ProdutoId == produtoId);

            estoque.DecrementarQuantidade(quantidade);

            _context.Update(estoque);

            await UnitOfWork.Salvar();
        }

        public async Task<PagedList<EstoqueDto>> ObterProdutosEmAbaixaNoEstoque(PaginationParams paginationParams)
        {
            var query = _context.Estoques.AsNoTracking()
                               .Include(e => e.Produto)
                               .Where(e => e.Quantidade <= e.Produto.EstoqueMinimo && e.Quantidade != 0)
                               .Select(estoque => new EstoqueDto
                               {
                                   ProdutoId = (Guid)estoque.ProdutoId,
                                   ProdutoNome = estoque.Produto.Nome,
                                   Quantidade = (int)estoque.Quantidade,
                                   Observacao = estoque.Observacao,
                                   DataCadastro = estoque.DataCadastro
                               });

            return await PagedList<EstoqueDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<PagedList<EstoqueDto>> ObterProdutosNoEstoques(PaginationParams paginationParams)
        {
            var query = _context.Estoques.AsNoTracking()
                                .Include(e => e.Produto)
                                .Select(estoque => new EstoqueDto
                                {
                                    ProdutoId = (Guid) estoque.ProdutoId,
                                    ProdutoNome = estoque.Produto.Nome,
                                    Quantidade = (int) estoque.Quantidade,
                                    Observacao = estoque.Observacao,
                                    DataCadastro = estoque.DataCadastro
                                });

            return await PagedList<EstoqueDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<PagedList<EstoqueDto>> ObterProdutosVaziosNoEstoque(PaginationParams paginationParams)
        {
            var query = _context.Estoques.AsNoTracking()
                               .Include(e => e.Produto)
                               .Where(e => e.Quantidade == 0)
                               .Select(estoque => new EstoqueDto
                               {
                                   ProdutoId = (Guid)estoque.ProdutoId,
                                   ProdutoNome = estoque.Produto.Nome,
                                   Quantidade = (int)estoque.Quantidade,
                                   Observacao = estoque.Observacao,
                                   DataCadastro = estoque.DataCadastro
                               });

            return await PagedList<EstoqueDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}
