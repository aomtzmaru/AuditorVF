using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegister userForRegister)
        {
            if (userForRegister == null) return BadRequest();

            if (await _repo.UserExist(userForRegister.PerId) > 0) return BadRequest("User already exists");

            UserForReturn userReturn = await _repo.Register(userForRegister);

            return Ok(userReturn);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLogin userForLogin)
        {
            if (userForLogin == null) return BadRequest();

            UserForReturn userForReturn = await _repo.Login(userForLogin);

            if (userForReturn == null) return Unauthorized("Username or Password not valid!");

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = _repo.GenerateToken(userForReturn);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}