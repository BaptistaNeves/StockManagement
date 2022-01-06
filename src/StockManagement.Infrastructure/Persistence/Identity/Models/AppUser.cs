using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace StockManagement.Infraestructure.Persistence.Identity.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Endereco { get; set; }
        public string NomeDeUtilizador { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
