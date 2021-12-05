using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManagement.Core.Entities.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Mappings.Vendas
{
    public class VendaMapping : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.ClienteId)
               .IsRequired(false);

            builder.Property(v => v.UsuarioId)
              .IsRequired(false);

            builder.Property(v => v.Total)
                .IsRequired(false)
                .HasColumnType("decimal(18,2)");

            builder.Property(v => v.Observacao)
               .IsRequired(false)
               .HasColumnType("varchar(max)");

            builder.Property(v => v.DataCadastro)
               .IsRequired()
               .HasColumnType("datetime2");

            builder.HasMany(v => v.VendaProdutos)
                .WithOne(vp => vp.Venda)
                .HasForeignKey(vp => vp.VendaId);

            builder.ToTable("Vendas");
        }
    }
}
