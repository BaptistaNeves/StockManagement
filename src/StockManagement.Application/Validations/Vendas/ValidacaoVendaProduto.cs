using FluentValidation;
using StockManagement.Application.InputModels.Vendas;

namespace StockManagement.Application.Validations.Vendas
{
    public class ValidacaoVendaProduto : AbstractValidator<VendaInputModel>
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
