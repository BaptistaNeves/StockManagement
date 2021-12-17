using System;
using System.ComponentModel.DataAnnotations;

namespace StockManagement.Application.InputModels.Vendas
{
    public class AnularVendaInputModel
    {
        [Required(ErrorMessage = "Selecione o produto da venda a ser anulada!")]
        public Guid ProdutoId { get; set; }

        [Required(ErrorMessage = "Informe a Quantidade do produto da venda a ser anulada!")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade do produto deve ser maior que zero!")]
        public int Quantidade { get; set; }
    }
}
