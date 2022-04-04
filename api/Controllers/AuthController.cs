using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Helpers;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, IMapper mapper)
        {
            _mapper = mapper;
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

        [HttpGet("GetUserDetail/{username}")]
        public async Task<IActionResult> GetUserDetail(string username)
        {
            if (username == "") return BadRequest();

            UserForReturn userForReturn = await _repo.GetUserDetail(username);

            if (userForReturn == null) return BadRequest();

            return Ok(userForReturn);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(UserForChangePassword user)
        {
            if (user == null) return BadRequest();
            if (string.IsNullOrEmpty(user.Password)) return BadRequest();
            if (string.IsNullOrEmpty(user.NewPassword)) return BadRequest();

            var result = await _repo.ChangePassword(user);
            return Ok(result);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserForUpdate user)
        {
            UserForReturn retUser = await _repo.UpdateUser(user);

            if (retUser == null) return BadRequest();

            return Ok(retUser);
        }

        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserList([FromQuery] UserParams userParams)
        {
            var extUserList = await _repo.List(userParams);
            ICollection<UserForReturn> extUserForReturn = _mapper.Map<ICollection<UserForReturn>>(extUserList);
            Response.AddPagination(extUserList.CurrentPage, extUserList.PageSize, extUserList.TotalCount, extUserList.TotalPages);
            return Ok(extUserForReturn);
        }
    }
}