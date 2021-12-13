using StockManagement.Core.Entities.Pessoa;
using System;
using System.Collections.Generic;

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

        public Venda(Guid? clienteId, Guid? usuarioId, double? total, string observacao)
        {
            ClienteId = clienteId;
            UsuarioId = usuarioId;
            Total = total;
            Observacao = observacao;
        }

        public Guid ObterIdDaVenda()
        {
            return Id;
        }

        public void CalcularOTotalDaVenda(double subTotalDaVenda)
        {
            Total = Total + subTotalDaVenda;
        }
    }
}
