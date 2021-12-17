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
        public bool? Status { get; set; }
        public DateTime DataCadastro { get; private set; }

        public Cliente Cliente { get; private set; }
        public ICollection<VendaProduto> VendaProdutos { get; private set; }

        public Venda(Guid? clienteId, Guid? usuarioId, string observacao)
        {
            ClienteId = clienteId;
            UsuarioId = usuarioId;
            Observacao = observacao;
        }

        public Guid ObterIdDaVenda() { return Id; } 
        public void CalcularOTotalDaVenda(double subTotalDaVenda)
        {
            if (Total == null) Total = 0;
            Total = Total + subTotalDaVenda;
        }

        public void AnularVenda() => Status = false;
    }
}
