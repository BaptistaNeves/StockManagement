using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManagement.Core.Entities.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Mappings.Catalogo
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {

            builder.HasKey(p => p.Id);

            builder.Property(p => p.CategoriaId)
               .IsRequired(false);

            builder.Property(p => p.Nome)
                .IsRequired(false)
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Preco)
                .IsRequired(false)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Descricao)
                .IsRequired(false)
                .HasColumnType("varchar(max)");

            builder.Property(p => p.Imagem)
                .IsRequired(false)
                .HasColumnType("varchar(255)");

            builder.Property(p => p.EstoqueMinimo)
                .IsRequired(false)
                .HasColumnType("int");

            builder.Property(p => p.Estocavel)
                .IsRequired(false)
                .HasColumnType("bit");

            builder.Property(p => p.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasMany(p => p.Vendas)
                .WithOne(v => v.Produto)
                .HasForeignKey(v => v.ProdutoId);

            builder.ToTable("Produtos");
        }
    }
}
