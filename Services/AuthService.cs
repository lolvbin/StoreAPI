using Microsoft.IdentityModel.Tokens;
using StoreAPI.Contracts;
using StoreAPI.DTOs;
using StoreAPI.Models;
using StoreAPI.Enums;
using System.Security.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreAPI.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(AppDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public Usuario Registrar(RegistrarUsuarioDTO dto)
    {
        //Verificar se o e-mail já existe no banco
        var emailExiste = _context.Usuarios.Any(u => u.Email.ToLower() == dto.Email.ToLower());
        if (emailExiste)
        {
            throw new Exception("Este e-mail já está cadastrado no sistema.");
        }

        // Por padrão o usuário, nasce como Cliente
        var tipoDefinido = TipoUsuario.Cliente;

        // Vamos tentar ler a Role de quem está logado fazendo a requisição de cadastro
        var usuarioLogadoRole = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;

        // Se quem estiver logado for um "Admin", aí sim nós permitimos criar com o Tipo que veio no DTO!
        if (usuarioLogadoRole == TipoUsuario.Admin.ToString())
        {
            tipoDefinido = dto.Tipo;
        }

        var novoUsuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = dto.Senha,
            Tipo = tipoDefinido
        };

        _context.Usuarios.Add(novoUsuario);
        _context.SaveChanges();

        return novoUsuario;
    }

    public string Login(LoginDTO dto)
    {
        // 1. Busca o usuário pelo e-mail
        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Email.ToLower() == dto.Email.ToLower());

        // 2. Se o usuário não existir OU a senha estiver incorreta
        if (usuario == null || usuario.Senha != dto.Senha)
        {
            // Lançamos uma exceção genérica de autenticação por segurança
            throw new InvalidCredentialException("E-mail ou senha incorretos.");
        }
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Tipo.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
