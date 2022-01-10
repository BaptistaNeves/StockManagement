using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManagement.Infraestructure.Persistence.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Mappings.Usuarios
{
    public class UsuarioMapping : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(c => c.Endereco)
                 .HasColumnType("varchar(300)");

            builder.Property(c => c.NomeDeUtilizador)
                 .HasColumnType("varchar(100)");
        }
    }
}
