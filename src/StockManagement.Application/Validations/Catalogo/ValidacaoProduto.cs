using FluentValidation;
using StockManagement.Application.InputModels.Catalogo;
using StockManagement.Core.Entities.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Application.Validations.Catalogo
{
    public class ValidacaoProduto : AbstractValidator<ProdutoInputModel>
    {
        public ValidacaoProduto()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser Informado!");

            RuleFor(p => p.Preco)
                .NotEmpty().WithMessage("O campo Preço deve ser Informado!")
                .GreaterThan(0).WithMessage("O Preço do produto deve ser maior que {ComparisonValue}!");

            RuleFor(p => p.Imagem)
                .NotEmpty().WithMessage("Selecione uma Imagem para o produto!");

            RuleFor(p => p.CategoriaId)
                .NotEmpty().WithMessage("Selecione uma Categoria para o produto!")
                .NotEqual(Guid.Empty).WithMessage("O Id da Categoria é inválido!");

            When(p => p.Estocavel == true, () =>
            {
                RuleFor(p => p.EstoqueMinimo)
                        .NotEmpty().WithMessage("Informe o estoque minimo do produto!")
                        .GreaterThan(0).WithMessage("O valor do estoque minimo deve ser maior que {ComparisonValue}!");

            });
                
        }
    }
}
