using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManagement.Core.Entities.Movimentacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Mappings.Movimentacao
{
    public class EstoqueMapping : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.ProdutoId)
                .IsRequired(false);

            builder.Property(e => e.UsuarioId)
                .IsRequired(false);

            builder.Property(e => e.Quantidade)
                .IsRequired(false)
                .HasColumnType("int");

            builder.Property(e => e.Observacao)
                .IsRequired(false)
                .HasColumnType("varchar(max)");

            builder.Property(e => e.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");

           builder.ToTable("Estoques");
        }
    }
}
