using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Application.InputModels.Movimentacao
{
    public class EstoqueInputModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Selecione o produto!")]
        public Guid ProdutoId { get; set; }

        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "Informe a quantidade do produto!")]
        public int Quantidade { get; set; }

        public string Observacao { get; set; }
    }
}
