using StockManagement.Core.Entities.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
