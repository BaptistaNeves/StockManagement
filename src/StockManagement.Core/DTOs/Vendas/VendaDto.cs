using System;

namespace StockManagement.Core.DTOs.Vendas
{
    public class VendaDto
    {
        public Guid Id { get; set; }
        public Guid? ProdutoId { get; set; }
        public string NomeDoProduto { get; set; }
        public string NomeDoCliente { get; set; }
        public string NomeDoUsuario { get; set; }
        public double? PrecoUnitario { get; set; }
        public int? Quantidade { get; set; }
        public double? SubTotal { get; set; }
        public double? Total { get; set; }
        public string Observacao { get; set; }
        public string Status { get; set; }
        public DateTime DataDaVenda { get; set; }
    }
}
