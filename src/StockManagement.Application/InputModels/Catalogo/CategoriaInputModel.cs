using System;
using System.ComponentModel.DataAnnotations;

namespace StockManagement.Application.InputModels.Catalogo
{
    public class CategoriaInputModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Descrição deve ser informado")]
        [MinLength(2, ErrorMessage = "O campo Descrição deve ter no minímo 2 caracteres!")]
        public string Descricao { get; set; }

    }
}
