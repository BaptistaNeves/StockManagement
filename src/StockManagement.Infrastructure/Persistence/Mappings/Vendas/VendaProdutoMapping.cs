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
    public class VendaProdutoMapping : IEntityTypeConfiguration<VendaProduto>
    {
        public void Configure(EntityTypeBuilder<VendaProduto> builder)
        {
            builder.HasKey(vp => vp.Id);

            builder.Property(vp => vp.ProdutoId)
                .IsRequired(false);

            builder.Property(vp => vp.VendaId)
                .IsRequired(false);

            builder.Property(vp => vp.Quantidade)
                .IsRequired(false)
                .HasColumnType("int");

            builder.Property(vp => vp.PrecoUnitario)
                .IsRequired(false)
                .HasColumnType("decimal(18,2)");

            builder.Property(vp => vp.Subtotal)
                .IsRequired(false)
                .HasColumnType("decimal(18,2)");

            builder.ToTable("VendaProdutos");
        }
    }
}
