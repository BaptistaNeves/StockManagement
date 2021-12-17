using StockManagement.Core.Entities.Catalogo;
using System;

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
        public Estoque(Guid? produtoId, Guid? usuarioId, int? quantidade, string observacao)
        {
            ProdutoId = produtoId;
            UsuarioId = usuarioId;
            Quantidade = quantidade;
            Observacao = observacao;
        }

        public void DecrementarQuantidade(int quantidade)
        {
            Quantidade = Quantidade - quantidade;
        }

        public void IncrementarQuantidade(int quantidade)
        {
            Quantidade = Quantidade + quantidade;
        }
    }
}
