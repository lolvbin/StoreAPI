using System;

namespace StoreAPI.DTOs;

    public record RegistrarUsuarioDTO(string Nome, string Email, string Senha, StoreAPI.Enums.TipoUsuario Tipo);
    public record LoginDTO(string Email, string Senha);

