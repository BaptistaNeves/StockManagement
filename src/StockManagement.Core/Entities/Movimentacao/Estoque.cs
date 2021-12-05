using StockManagement.Core.Entities.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Core.Entities.Movimentacao
{
    public class Estoque : Entidade
    {
        public Guid? ProdutoId { get; private set; }
        public Guid? UsuarioId { get; private set; }
        public int? Quantidade { get; private set; }
        public string Observacao { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public Produto Produto { get; private set; }
    }
}
