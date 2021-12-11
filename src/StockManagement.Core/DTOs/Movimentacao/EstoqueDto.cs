using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Core.DTOs.Movimentacao
{
    public class EstoqueDto
    {
        public Guid ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public string UsuarioNome { get; set; }
        public int Quantidade { get; set; }
        public string Observacao { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
