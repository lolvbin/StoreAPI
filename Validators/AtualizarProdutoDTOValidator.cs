using System;
using FluentValidation;
using FluentValidation.Validators;
using StoreAPI.DTO;

namespace StoreAPI;

public class AtualizarProdutoDTOValidator : AbstractValidator<AtualizarProdutoDTO>
{
    public AtualizarProdutoDTOValidator()
    {
        RuleFor(x => x.Name).
        NotEmpty().WithMessage("Nome do produto não pode ser vazio!").
        MinimumLength(3).WithMessage("Nome do produto deve conter no minimo 3 letras!");
        
        RuleFor(x => x.Price).
        GreaterThan(0).WithMessage("O preço do produto deve ser superior a R$ 0,00 reais!");

        RuleFor(x => x.Stock).
        GreaterThanOrEqualTo(0).WithMessage("O estoque não pode ser menor que 0!").
        LessThan(99).WithMessage("Não é possível adicionar mais que 99 produtos no estoque!");

    }
}
