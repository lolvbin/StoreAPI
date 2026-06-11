using System;
using FluentValidation;
using FluentValidation.Results;
using StoreAPI.Models;

namespace StoreAPI;

public class ProdutoValidator : AbstractValidator<Produto>
{
    public ProdutoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome do produto não pode ser vazio!")
                .MinimumLength(3).WithMessage("Nome do produto deve conter no minimo 3 letras!");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("O preço do produto deve ser superior a R$ 0,00 reais!");

            RuleFor(x => x.Stock)
                .GreaterThan(0).WithMessage("O estoque inicial não pode ser 0!");
        }
}
