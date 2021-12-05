using FluentValidation;
using StockManagement.Core.Entities.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Application.Validations.Vendas
{
    public class ValidacaoVenda : AbstractValidator<Venda>
    {
        public ValidacaoVenda()
        {
            RuleFor(v => v.ClienteId)
                .NotEmpty().WithMessage("O campo cliente deve ser fornecido!");
        }
    }
}
