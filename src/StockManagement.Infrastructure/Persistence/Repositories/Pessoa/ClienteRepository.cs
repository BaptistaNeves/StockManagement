using Microsoft.EntityFrameworkCore;
using StockManagement.Core.Entities.Pessoa;
using StockManagement.Core.Interfaces.Persistence.Repositories.Generico;
using StockManagement.Core.Interfaces.Persistence.Repositories.Pessoa;
using StockManagement.Infrastructure.Persistence.Context;
using StockManagement.Infrastructure.Persistence.Repositories.Generico;
using StockManagement.Core.DTOs.Pessoa;
using StockManagement.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Repositories.Pessoa
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context){}
        public IUnitOfWork unitOfWork => _context;

        public async Task<Cliente> ObterClienteVendasPorId(Guid id)
        {
            return await _context.Clientes.AsNoTracking()
                            .Include(c => c.Vendas)
                            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<PagedList<ClienteDto>> ObterClientes(PaginationParams paginationParams)
        {
            var query = _context.Clientes.AsNoTracking()
                            .Select(cliente => new ClienteDto
                            {
                                Id = cliente.Id,
                                Nome = cliente.Nome,
                                Email = cliente.Email,
                                Telefone = cliente.Telefone,
                                Endereco = cliente.Endereco,
                                DataCadastro = cliente.DataCadastro,
                                Observacao = cliente.Observacao
                            });

            return await PagedList<ClienteDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}
