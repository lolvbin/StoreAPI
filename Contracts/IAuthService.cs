using System;
using StoreAPI.DTOs;
using StoreAPI.Models;

namespace StoreAPI.Contracts;

public interface IAuthService
{
    Usuario Registrar(RegistrarUsuarioDTO dto);
    string Login(LoginDTO dto);
}
