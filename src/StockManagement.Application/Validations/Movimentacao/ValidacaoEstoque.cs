using FluentValidation;
using StockManagement.Core.Entities.Movimentacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Application.Validations.Movimentacao
{
    public class ValidacaoEstoque : AbstractValidator<Estoque>
    {
        public ValidacaoEstoque()
        {
            RuleFor(e => e.ProdutoId)
                .NotEmpty().WithMessage("Nenhum produto foi selecionado!");

            RuleFor(e => e.Quantidade)
                .NotEmpty().WithMessage("Informe a quantidade do produto selecionado para o estoque!");
        }
    }
}
