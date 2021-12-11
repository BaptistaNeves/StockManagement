using System;
using System.Collections.Generic;

namespace StockManagement.Core.DTOs.Catalogo
{
    public class CategoriaDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public ICollection<ProdutoDto> Produtos { get; set; }
    }
}
