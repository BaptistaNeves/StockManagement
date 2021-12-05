using FluentValidation;
using StockManagement.Core.Entities.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Application.Validations.Vendas
{
    public class ValidacaoVendaProduto : AbstractValidator<VendaProduto>
    {
        public ValidacaoVendaProduto()
        {
            RuleFor(vp => vp.ProdutoId)
                .NotEmpty().WithMessage("Nenhum produto foi selecionado!");

            RuleFor(vp => vp.Quantidade)
                .NotEmpty().WithMessage("Informe a Quantidade do produto selecionado!");
        }
    }
}
