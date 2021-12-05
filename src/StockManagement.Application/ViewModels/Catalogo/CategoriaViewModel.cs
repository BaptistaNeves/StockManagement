using System;
using System.Collections.Generic;

namespace StockManagement.Application.ViewModels.Catalogo
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public ICollection<ProdutoViewModel> Produtos { get; set; }

    }
}
