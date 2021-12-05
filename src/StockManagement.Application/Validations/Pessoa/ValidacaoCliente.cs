using FluentValidation;
using StockManagement.Application.InputModels.Pessoa;

namespace StockManagement.Application.Validations.Pessoa
{
    public class ValidacaoCliente : AbstractValidator<ClienteInputModel>
    {
        public ValidacaoCliente()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo Nome deve ser informado!");

            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("O campo Telefone deve ser informado!");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O campo Email deve ser informado!")
                .EmailAddress().WithMessage("O email informado não é válido!");

            RuleFor(c => c.Endereco)
                .NotEmpty().WithMessage("O campo Endereço deve ser informado!")
                .MinimumLength(20).WithMessage("O campo Endereço deve ter no minimo { ComparisonValue} caracteres!")
                .MaximumLength(500).WithMessage("O campo Endereço deve ter no máximo {ComparisonValue} caracteres!");
        }
    }
}
