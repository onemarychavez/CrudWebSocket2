using CrudWebSocket2.Funtion;
using CrudWebSocket2.Models;
using CrudWebSocket2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudWebSocket2.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        // Constructor correcto
        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuariosModels user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { message = "Usuario y contraseña son obligatorios" });
            }

            string adminPasswordEncript = "e10adc3949ba59abbe56e057f20f883e";
            string generalPasswordEncript = "adcd7048512e64b48da55b027577886ee5a36350";

            string passwordNueva = HashHelper.ComputeMD5(user.Password);

            if (user.Username == "admin")
            {
                if (passwordNueva == adminPasswordEncript)
                {
                    var token = _jwtService.GenerateToken(user.Username, "Admin");
                    return Ok(new { token });
                }
                return Unauthorized(new { message = "Contraseña incorrecta para admin" });
            }
            else if (user.Username == "general")
            {
                if (passwordNueva == generalPasswordEncript)
                {
                    var token = _jwtService.GenerateToken(user.Username, "General");
                    return Ok(new { token });
                }
                return Unauthorized(new { message = "Contraseña incorrecta para usuario general" });
            }

            return Unauthorized(new { message = "Usuario no encontrado" });
        }
    }
}
