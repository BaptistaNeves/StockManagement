using StockManagement.Core.Entities.Catalogo;
using System;

namespace StockManagement.Core.Entities.Vendas
{
    public class VendaProduto : Entidade
    {
        public Guid? ProdutoId { get; private set; }
        public Guid? VendaId { get; private set; }
        public int? Quantidade { get; private set; }
        public double? PrecoUnitario { get; private set; }
        public double? Subtotal { get; private set; }

        public Produto Produto { get; private set; }
        public Venda Venda { get; private set; }

        public VendaProduto(Guid? produtoId, Guid? vendaId, int?
            quantidade, double? precoUnitario, double? subtotal)
        {
            ProdutoId = produtoId;
            VendaId = vendaId;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            Subtotal = subtotal;
        }

        public void CalcularOSubTotalDaVenda()
        {
            Subtotal = PrecoUnitario * Quantidade;
        }

        public void AtribuirOIdDaVenda(Guid id)
        {
            VendaId = id;
        }

    }
}
