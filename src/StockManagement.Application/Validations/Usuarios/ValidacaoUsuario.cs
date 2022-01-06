using FluentValidation;
using StockManagement.Application.InputModels.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Application.Validations.Usuarios
{
    public class ValidacaoUsuario : AbstractValidator<UsuarioInputModel>
    {
        public ValidacaoUsuario()
        {
            RuleFor(u => u.NomeDeUtilizador)
                 .NotEmpty().WithMessage("O Nome do Úsuario deve ser informado!");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O Email do Úsuario deve ser informado!")
                .EmailAddress().WithMessage("O email é Inválido!");

            RuleFor(u => u.Telefone)
                 .NotEmpty().WithMessage("O Telefone do Úsuario deve ser informado!");

            RuleFor(u => u.Telefone)
                .NotEmpty().WithMessage("O Endereço do Úsuario deve ser informado!");

            RuleFor(u => u.Senha)
                .MinimumLength(6).WithMessage("A senha deve ter no minímo 6 caracteres");

        }
    }
}
