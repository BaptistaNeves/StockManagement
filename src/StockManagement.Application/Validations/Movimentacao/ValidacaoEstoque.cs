using FluentValidation;
using StockManagement.Application.InputModels.Movimentacao;

namespace StockManagement.Application.Validations.Movimentacao
{
    public class ValidacaoEstoque : AbstractValidator<EstoqueInputModel>
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
