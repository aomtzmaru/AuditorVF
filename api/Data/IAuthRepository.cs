using System.Threading.Tasks;
using api.Dtos;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Data
{
    public interface IAuthRepository
    {
         Task<UserForReturn> Register(UserForRegister user);
         Task<int> UserExist(string username);
         Task<UserForReturn> Login(UserForLogin user);
         SecurityToken GenerateToken(UserForReturn user);
    }
}