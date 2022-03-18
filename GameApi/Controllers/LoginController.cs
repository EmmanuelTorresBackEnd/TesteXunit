using GameApi.Interfaces;
using GameApi.Models;
using GameApi.Repositories;
using GameApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GameApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
                Usuario UsuarioBuscado = _usuarioRepository.Login(login.Email, login.Senha);

                if (UsuarioBuscado == null)
                {
                   // return NotFound("e-mail ou senha inválidos!");

                    return Unauthorized(new {msg = "e-mail ou senha inválidos!" });
                }

                var minhasClaims = new[] {
                    new Claim(JwtRegisteredClaimNames. Email, UsuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, UsuarioBuscado.Id.ToString()),
                    new Claim(ClaimTypes.Role, UsuarioBuscado.Tipo.ToString())
                    };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("api-game-autenticação"));
                //cred = credenciais
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var MeuToken = new JwtSecurityToken(
                    issuer: "game.web.api",
                    audience: "game.web.api",
                    claims: minhasClaims,
                    expires: DateTime.Now.AddHours(24),
                    signingCredentials: cred
                    
                    );

                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(MeuToken)
                    }

                );

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
