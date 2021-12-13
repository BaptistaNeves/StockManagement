using System;
using System.ComponentModel.DataAnnotations;

namespace StockManagement.Application.InputModels.Vendas
{
    public class VendaInputModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O cliente que efetua à compra deve ser informado!")]
        public Guid ClienteId { get; set; }

        public Guid UsuarioId { get; set; }

        public string Observacao { get; set; }

        [Required(ErrorMessage = "Selecione o produto para realizar a venda!")]
        public Guid ProdutoId { get; set; }

        [Required(ErrorMessage = "Informe a Quantidade do produtoa ser vendido!")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade do produto deve ser maior que zero!")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Informe o preço do produto!")]
        public double PrecoUnitario { get; set; }
    }
}
