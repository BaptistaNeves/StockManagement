using FluentValidation;
using StockManagement.Application.InputModels.Vendas;

namespace StockManagement.Application.Validations.Vendas
{
    public class ValidacaoVenda : AbstractValidator<VendaInputModel>
    {
        public ValidacaoVenda()
        {
            RuleFor(v => v.ClienteId)
                .NotEmpty().WithMessage("O campo cliente deve ser fornecido!");

            RuleFor(vp => vp.ProdutoId)
                .NotEmpty().WithMessage("Nenhum produto foi selecionado!");

            RuleFor(vp => vp.Quantidade)
                .NotEmpty().WithMessage("Informe a Quantidade do produto selecionado!");
        }
    }
}
