using System;

namespace StockManagement.Core.DTOs.Usuario
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Perfil { get; set; }
    }
}
