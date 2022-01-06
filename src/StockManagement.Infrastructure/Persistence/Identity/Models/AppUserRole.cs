using Microsoft.AspNetCore.Identity;
using System;

namespace StockManagement.Infraestructure.Persistence.Identity.Models
{
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public AppUser Usuario { get; set; }
        public AppRole Role { get; set; }
    }
}
