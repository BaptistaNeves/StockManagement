using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Application.ViewModels.Catalogo
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public bool Estocavel { get; set; }
        public int EstoqueMinimo { get; set; }
        public string Imagem { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }

        public CategoriaViewModel Categoria { get; private set; }
    }
}
