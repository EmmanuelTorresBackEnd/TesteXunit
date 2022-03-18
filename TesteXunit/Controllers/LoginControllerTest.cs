using GameApi.Controllers;
using GameApi.Interfaces;
using GameApi.Models;
using GameApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TesteXunit.Controllers
{
    public class LoginControllerTest
    {
        [Fact]
        public void LoginController_RetornarUsuarioInvalido()
        {
            //Arrange
            var RepositorioFalso = new Mock<IUsuarioRepository>();

            RepositorioFalso.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);

            LoginViewModel DadosUsuario = new LoginViewModel();
            DadosUsuario.Email = "email@email.com";
            DadosUsuario.Senha = "123456";

            var Controller = new LoginController(RepositorioFalso.Object);

            //Act
            var resultado = Controller.Login(DadosUsuario);

            //Assert
            Assert.IsType<UnauthorizedObjectResult>(resultado);

        }

        [Fact]
        public void LoginController_RetornarUsuario()
        {
            //Arrange
            string IssuerValidacao = "game.web.api";

            Usuario usuarioFake = new Usuario();
            usuarioFake.Email = "email@email.com";
            usuarioFake.Senha = "123456";

            var RepositorioFalso = new Mock<IUsuarioRepository>();
            RepositorioFalso.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioFake);

            var Controller = new LoginController(RepositorioFalso.Object);

            LoginViewModel dadosUsuario = new LoginViewModel();
            dadosUsuario.Email = "email@email.com";
            dadosUsuario.Senha = "123456";

            //Act
            OkObjectResult resultado = (OkObjectResult)Controller.Login(dadosUsuario);

            var token = resultado.Value.ToString().Split(' ')[3];

            var jstHandler = new JwtSecurityTokenHandler();

            var jwtToken = jstHandler.ReadJwtToken(token);

            //Assert
            Assert.Equal(IssuerValidacao, jwtToken.Issuer);


        }

    }
}
