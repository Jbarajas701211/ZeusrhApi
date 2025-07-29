using AutoMapper;
using Interfaces.IBussiness;
using Interfaces.Utilitys;
using Models;
using Models.DTOs;
using Models.Entities;
using Moq;
using ZeusrhApi.Controllers;

namespace ZeusrhApiTest
{
    public class UsuarioControllerTests
    {
        private readonly Mock<IManagementUsuario> _mockManagementUsuario;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUtility> _mockUtility;
        private readonly UsuarioController _controller;

        public UsuarioControllerTests()
        {
            _mockManagementUsuario = new Mock<IManagementUsuario>();
            _mockMapper = new Mock<IMapper>();
            _mockUtility = new Mock<IUtility>();

            _controller = new UsuarioController(
                _mockMapper.Object,
                _mockManagementUsuario.Object,
                _mockUtility.Object);
        }

        [Fact]
        public async Task Registrarse_ReturnsSuccessResponse_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var usuarioDTO = new UsuarioDTO { Clave = "password", /* otros campos */ };
            var usuarioEncriptado = "hashedPassword";
            var usuarioModel = new Usuario { /* asigna propiedades */ };
            var respuestaAutenticacionDTO = new RespuestaAutenticacionDTO() { Expiracion = DateTime.Now, Token = "123" };

            _mockUtility.Setup(u => u.encriptarSHA256(It.IsAny<string>()))
                        .Returns(usuarioEncriptado);

            _mockMapper.Setup(m => m.Map<Usuario>(It.IsAny<UsuarioDTO>()))
                       .Returns(usuarioModel);

            var apiResponse = new ApiResponse<RespuestaAutenticacionDTO>
            {
                Success = true,
                Data = respuestaAutenticacionDTO
            };

            _mockManagementUsuario.Setup(m => m.RegistroUsuario(It.IsAny<Usuario>()))
                                  .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.Registrarse(usuarioDTO);

            // Assert
            var okResult = Assert.IsType<ApiResponse<RespuestaAutenticacionDTO>>(result);
            Assert.True(okResult.Success);
            Assert.Equal(respuestaAutenticacionDTO, okResult.Data);
        }

        [Fact]
        public async Task Registrarse_ReturnsErrorResponse_WhenExceptionIsThrown()
        {
            // Arrange
            var usuarioDTO = new UsuarioDTO { Clave = "password" };

            _mockUtility.Setup(u => u.encriptarSHA256(It.IsAny<string>()))
                        .Returns("hashedPassword");

            _mockMapper.Setup(m => m.Map<Usuario>(It.IsAny<UsuarioDTO>()))
                       .Returns(new Usuario());

            _mockManagementUsuario.Setup(m => m.RegistroUsuario(It.IsAny<Usuario>()))
                                  .ThrowsAsync(new System.Exception("Error al registrar"));

            // Act
            var result = await _controller.Registrarse(usuarioDTO);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Error al registrar", result.Errors);
        }

        [Fact]
        public async Task Login_ReturnsSuccessResponse_WhenLoginIsSuccessful()
        {
            // Arrange
            var loginDTO = new LoginDTO { Clave = "password", Correo = "jbarajas70@msn.com" };
            var claveEncriptada = "hashedPassword";
            var respuestaAutenticacionDTO = new RespuestaAutenticacionDTO() { Expiracion = DateTime.Now, Token = "1234" };

            // Configuramos el mock para encriptar la clave
            _mockUtility.Setup(u => u.encriptarSHA256(It.IsAny<string>()))
                        .Returns(claveEncriptada);

            // Configuramos el mock para devolver una respuesta exitosa del login
            var apiResponse = new ApiResponse<RespuestaAutenticacionDTO>
            {
                Success = true,
                Data = respuestaAutenticacionDTO
            };

            _mockManagementUsuario.Setup(m => m.Login(It.Is<LoginDTO>(l => l.Clave == claveEncriptada)))
                                  .ReturnsAsync(apiResponse);

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            var okResult = Assert.IsType<ApiResponse<RespuestaAutenticacionDTO>>(result);
            Assert.True(okResult.Success);
            Assert.Equal(respuestaAutenticacionDTO, okResult.Data);
        }

        [Fact]
        public async Task Login_ThrowsException_WhenManagementThrows()
        {
            // Arrange
            var loginDTO = new LoginDTO { Clave = "password", Correo = "jbarajas70@msn.com" };

            _mockUtility.Setup(u => u.encriptarSHA256(It.IsAny<string>()))
                        .Returns("hashedPassword");

            _mockManagementUsuario.Setup(m => m.Login(It.IsAny<LoginDTO>()))
                                  .ThrowsAsync(new System.Exception("Error en login"));

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(() => _controller.Login(loginDTO));
        }
    }
}
