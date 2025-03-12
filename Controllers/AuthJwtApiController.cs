using CrudWebSocket2.Funtion;
using CrudWebSocket2.Models;
using CrudWebSocket2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudWebSocket2.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(JwtService jwtService) : ControllerBase
    {
        private readonly JwtService _jwtService = jwtService;

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuariosModels user)
        {
            string adminPasswordEncript = "e10adc3949ba59abbe56e057f20f883e";

            string generalPasswordEncript = "adcd7048512e64b48da55b027577886ee5a36350";

            string passwordNueva = HashHelper.ComputeMD5(user.Password);
            
            if (user.Username == "admin" && adminPasswordEncript == passwordNueva)
            {
                var token = _jwtService.GenerateToken(user.Username, "Admin");
                return Ok(new { token });
            }else if (user.Username == "general" && generalPasswordEncript == passwordNueva)
            {
                var token = _jwtService.GenerateToken(user.Username, "General");
                return Ok(new { token });
            }



            return Unauthorized(new { message = "Credenciales incorrectas" });
        }
    }
}
