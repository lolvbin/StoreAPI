using Microsoft.AspNetCore.Mvc;
using StoreAPI.Contracts;
using StoreAPI.DTOs;
using StoreAPI.Responses;
using Microsoft.AspNetCore.Authorization;
using SQLitePCL;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

    [HttpPost("registrar")]
    [AllowAnonymous]
    public IActionResult Registrar([FromBody] RegistrarUsuarioDTO dto)
    {
        var novoUsuario = _authService.Registrar(dto);
        var resposta = new APIResponse<object>(true, "Usuário cadastrado com sucesso!", novoUsuario);
        return Created("", resposta);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO dto)
    {
        try
        {
            var resultadoToken = _authService.Login(dto);

            var resposta = new APIResponse<string>(true, "Login realizado com sucesso!", resultadoToken);
            return Ok(resposta);
        }
        catch(Exception ex)
        {
            var respostaErro = new APIResponse<object>(false, ex.Message, null);
            return Unauthorized(respostaErro);
        }
    }
}
