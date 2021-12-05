using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockManagement.Core.Entities.Pessoa;

namespace StockManagement.Core.Entities.Vendas
{
    public class Venda : Entidade
    {
        public Guid? ClienteId { get; private set; }
        public Guid? UsuarioId { get; private set; }
        public double? Total { get; private set; }
        public string Observacao { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public Cliente Cliente { get; private set; }
        public ICollection<VendaProduto> VendaProdutos { get; private set; }
    }
}
