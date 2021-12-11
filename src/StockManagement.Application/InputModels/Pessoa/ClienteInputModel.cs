using System;
using System.ComponentModel.DataAnnotations;

namespace StockManagement.Application.InputModels.Pessoa
{
    public class ClienteInputModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser informado!")]
        [MinLength(2, ErrorMessage = "O campo {0} deve ter no minimo {1} caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser informado!")]        
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser informado!")]
        [EmailAddress(ErrorMessage = "O email informado é inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser informado!")]
        [MinLength(20, ErrorMessage = "O campo Enderço deve ter no minimo {1} caracteres!")]
        public string Endereco { get; set; }

        public string Observacao { get; set; }

    }
}
