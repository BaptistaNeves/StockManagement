using System;
using System.ComponentModel.DataAnnotations;

namespace StockManagement.Application.InputModels.Usuarios
{
    public class UsuarioInputModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório!")]
        public string NomeDeUtilizador { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório!")]
        [EmailAddress(ErrorMessage = "Email Inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        public string Endereco { get; set; }

        [StringLength(100, ErrorMessage = "O campo {0} precisa ter no minimo 6 carateres!", MinimumLength = 6)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem!")]
        public string ConfirmarSenha { get; set; }

        public string Perfil { get; set; }
    }
}
