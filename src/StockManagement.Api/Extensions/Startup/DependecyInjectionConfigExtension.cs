using Microsoft.Extensions.DependencyInjection;
using StockManagement.Application.Interface.Services.Catalogo;
using StockManagement.Application.Interface.Services.Movimentacao;
using StockManagement.Application.Interface.Services.Pessoa;
using StockManagement.Application.Interface.Services.Vendas;
using StockManagement.Application.Interfaces.Notification;
using StockManagement.Application.Notification;
using StockManagement.Application.Services.Catalogo;
using StockManagement.Application.Services.Movimentacao;
using StockManagement.Application.Services.Pessoa;
using StockManagement.Application.Services.Vendas;
using StockManagement.Core.Interfaces.Persistence.Repositories.Catalogo;
using StockManagement.Core.Interfaces.Persistence.Repositories.Movimentacao;
using StockManagement.Core.Interfaces.Persistence.Repositories.Pessoa;
using StockManagement.Core.Interfaces.Persistence.Repositories.Vendas;
using StockManagement.Infrastructure.Persistence.Context;
using StockManagement.Infrastructure.Persistence.Repositories.Catalogo;
using StockManagement.Infrastructure.Persistence.Repositories.Movimentacao;
using StockManagement.Infrastructure.Persistence.Repositories.Pessoa;
using StockManagement.Infrastructure.Persistence.Repositories.Vendas;

namespace StockManagement.Api.Extensions.Startup
{
    public static class DependecyInjectionConfigExtension
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IVendaRepository, VendaRepository>();
            services.AddScoped<IVendaProdutoRepository, VendaProdutoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEstoqueRepository, EstoqueRepository>();

            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddScoped<IClienteService, ClienteService>();

            services.AddScoped<IEstoqueService, EstoqueService>();

            services.AddScoped<IVendaService, VendaService>();

            return services;
        }
    }
}
