using System;
using System.Collections.Generic;

namespace StockManagement.Core.Entities.Catalogo
{
    public class Categoria : Entidade
    {
        public string Descricao { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public ICollection<Produto> Produtos { get; private set; }

        public Categoria(string descricao)
        {
            Descricao = descricao;
        }
    }
}
