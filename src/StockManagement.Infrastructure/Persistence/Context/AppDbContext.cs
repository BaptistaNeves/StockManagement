using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockManagement.Core.Entities.Catalogo;
using StockManagement.Core.Entities.Movimentacao;
using StockManagement.Core.Entities.Pessoa;
using StockManagement.Core.Entities.Vendas;
using StockManagement.Core.Interfaces.Persistence.Repositories.Generico;
using StockManagement.Infraestructure.Persistence.Identity.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid,
                                IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
                                IdentityUserToken<Guid>>, IUnitOfWork
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<AppRole>()
                       .HasMany(r => r.UserRoles)
                       .WithOne(ur => ur.Role)
                       .HasForeignKey(ur => ur.RoleId)
                       .IsRequired();

            modelBuilder.Entity<AppUser>()
                       .HasMany(u => u.UserRoles)
                       .WithOne(ur => ur.Usuario)
                       .HasForeignKey(ur => ur.UserId)
                       .IsRequired();

            foreach (var relacao in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relacao.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

        }

        public async Task<bool> Salvar()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }

        private void DefenirRelacionamentoDoIdentity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppRole>()
                       .HasMany(r => r.UserRoles)
                       .WithOne(ur => ur.Role)
                       .HasForeignKey(ur => ur.RoleId)
                       .IsRequired();

            modelBuilder.Entity<AppUser>()
                       .HasMany(u => u.UserRoles)
                       .WithOne(ur => ur.Usuario)
                       .HasForeignKey(ur => ur.UserId)
                       .IsRequired();
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaProduto> VendaProdutos { get; set; }
    }
}
