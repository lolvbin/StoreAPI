using System;
using System.Data;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using RealDougAPI.DTO;

namespace RealDougAPI;

public class CriarPedidoDTOValidator : AbstractValidator<CriarPedidoDTO>
{
    public CriarPedidoDTOValidator()
    {
        RuleFor(x => x.ProdutosIds).
        NotEmpty().WithMessage("O Id dos produtos não pode ser vazio!")
        .Must(ids => ids.All(id => id != Guid.Empty)).WithMessage("Todos os Ids dos produtos devem ser maiores que zero!");

        RuleFor(x => x.Status).
        NotEmpty().WithMessage("O status do pedido não pode ser vazio");
    }
}
