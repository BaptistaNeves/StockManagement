using FluentValidation;
using StockManagement.Application.InputModels.Catalogo;
using StockManagement.Core.Entities.Catalogo;

namespace StockManagement.Application.Validations.Catalogo
{
    public class ValidacaoCategoria : AbstractValidator<CategoriaInputModel>
    {
        public ValidacaoCategoria()
        {
            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("O campo Descrição deve ser informado!")
                .Length(2, 50).WithMessage("O campo Descrição precisa ter entre {MinLength} e {MaxLength} caracteres!");
        }
    }
}
