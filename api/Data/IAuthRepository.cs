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
         Task<UserForReturn> GetUserDetail(string username);
         SecurityToken GenerateToken(UserForReturn user);
    }
}