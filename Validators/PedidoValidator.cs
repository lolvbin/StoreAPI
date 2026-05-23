using System;
using FluentValidation;
using FluentValidation.Results;
using RealDougAPI.Models;

namespace RealDougAPI;

public class PedidoValidator : AbstractValidator<Pedido>
{
    public PedidoValidator()
    {
        RuleFor(x => x.Data)
            .NotEmpty().WithMessage("Data do pedido não pode ser vazia!")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Data do pedido não pode ser futura!");

        RuleFor(x => x.Produtos)
            .NotEmpty().WithMessage("O pedido deve conter pelo menos um produto!");
    }
}
