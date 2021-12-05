using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManagement.Core.Entities.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Mappings.Pessoa
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired(false)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Telefone)
                .IsRequired(false)
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Email)
                .IsRequired(false)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Endereco)
                .IsRequired(false)
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Observacao)
                .IsRequired(false)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.DataCadastro)
              .IsRequired()
              .HasColumnType("datetime2");

            builder.HasMany(c => c.Vendas)
                .WithOne(v => v.Cliente)
                .HasForeignKey(v => v.ClienteId);

            builder.ToTable("Clientes");
        }
    }
}
