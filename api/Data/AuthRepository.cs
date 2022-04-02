using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthRepository(DataContext context, IMapper mapper, IConfiguration config)
        {
            _config = config;
            _mapper = mapper;
            _context = context;

        }
        public async Task<UserForReturn> Register(UserForRegister userForRegister)
        {
            User user = new User();

            user = _mapper.Map<User>(userForRegister);
            user.Username = userForRegister.PerId;
            user.Role = "user";
            user.Created = DateTime.Now;
            user.Deleted = 0;

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userForRegister.Password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            InsertLog(userForRegister.PerId, "Register Success", "Regisster");

            UserForReturn userForReturn = _mapper.Map<UserForReturn>(user);

            return userForReturn;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<int> UserExist(string username)
        {
            List<User> u = await _context.User.Where(u => u.Username == username && u.Deleted == 0).ToListAsync();
            return u.Count;
        }

        public async Task<UserForReturn> Login(UserForLogin userForLogin)
        {
            User user = await _context.User.Where(u => u.Username == userForLogin.username).FirstOrDefaultAsync();
            if (user == null)
            {
                InsertLog(userForLogin.username, "Login failed", "Login");
                return null;
            }

            bool verifyResult = VerifyPasswordHash(userForLogin.password, user.PasswordHash, user.PasswordSalt);
            if (verifyResult == false)
            {
                InsertLog(userForLogin.username, "Login failed", "Login");
                return null;
            }

            InsertLog(userForLogin.username, "Login success", "Login");

            UserForReturn userForReturn = _mapper.Map<UserForReturn>(user);
            return userForReturn;
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async void InsertLog(string username, string ActionDetail, string PageAction)
        {
            Log logData = new Log();
            logData.Username = username;
            logData.ActionDetail = ActionDetail;
            logData.PageAction = PageAction;
            logData.Created = DateTime.Now;
            await _context.Log.AddAsync(logData);
            await _context.SaveChangesAsync();
        }

        public SecurityToken GenerateToken(UserForReturn user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(6),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}