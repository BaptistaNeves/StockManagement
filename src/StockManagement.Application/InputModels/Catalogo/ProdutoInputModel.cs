using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace StockManagement.Application.InputModels.Catalogo
{
    public class ProdutoInputModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Categoria deve ser informado!")]
        public Guid CategoriaId { get; set; }

        [Required(ErrorMessage = "O campo Nome deve ser informado!")]
        [MinLength(2, ErrorMessage = "O campo Nome deve ter no minímo 2 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Preço deve ser informado!")]
        [Range(1, int.MaxValue, ErrorMessage = "O valor do Preço não poder ser Zero!")]
        public double Preco { get; set; }

        public bool Estocavel { get; set; }

        public int EstoqueMinimo { get; set; }

        public string Imagem { get; set; }

        public IFormFile ArquivoEnviado { get; set; }

        public string Descricao { get; set; }
    }
}
