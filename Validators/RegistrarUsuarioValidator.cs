using System;
using FluentValidation;
using StoreAPI.DTOs;

namespace StoreAPI.Validators;

public class RegistrarUsuarioValidator : AbstractValidator<RegistrarUsuarioDTO>
{
    public RegistrarUsuarioValidator()
    {
        RuleFor(u => u.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.")
            .MaximumLength(50).WithMessage("O nome deve ter no máximo 50 caracteres.");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("Informe um e-mail válido.")
            .MaximumLength(100).WithMessage("O e-mail deve ter no máximo 100 caracteres.");

        RuleFor(u => u.Senha)
            .NotEmpty().WithMessage("A senha é obrigatório.")
            .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");

        RuleFor(u => u.Tipo)
            .IsInEnum().WithMessage("Tipo de usuário inválido (Deve ser 1, 2 ou 3).");
    }
}
