using System;
using System.Collections.Generic;

namespace StockManagement.Core.DTOs.Usuario
{
    public class UsuarioComRolesDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
