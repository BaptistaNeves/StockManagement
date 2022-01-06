using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace StockManagement.Infraestructure.Persistence.Identity.Models
{
    public class AppRole : IdentityRole<Guid>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
