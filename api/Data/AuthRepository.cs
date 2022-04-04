using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Dtos;
using api.Helpers;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _http;
        public AuthRepository(DataContext context, IMapper mapper, IConfiguration config, IHttpContextAccessor http)
        {
            _http = http;
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
            user.CreatedIp = _http.HttpContext.Connection.RemoteIpAddress.ToString();

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
            logData.IP = _http.HttpContext.Connection.RemoteIpAddress.ToString();
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

        public async Task<UserForReturn> GetUserDetail(string username)
        {
            User user = await _context.User.FirstOrDefaultAsync(u => u.Username == username);

            UserForReturn userForReturn = _mapper.Map<UserForReturn>(user);

            return userForReturn;
        }

        public async Task<bool> ChangePassword(UserForChangePassword user)
        {
            var username = GetUserNameFromToken();

            if (username == null) return false;

            User dbUser = await _context.User.Where(u => u.Username == username && u.Deleted == 0).FirstOrDefaultAsync();

            if (dbUser == null) return false;

            var validPwd = VerifyPasswordHash(user.Password, dbUser.PasswordHash, dbUser.PasswordSalt);

            if (!validPwd) return false;

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.NewPassword, out passwordHash, out passwordSalt);

            dbUser.PasswordHash = passwordHash;
            dbUser.PasswordSalt = passwordSalt;

            _context.SaveChanges();

            return true;
        }

        public string GetUserNameFromToken()
        {
            HttpRequest request = _http.HttpContext.Request;
            if (request == null)
                return null;

            JwtSecurityToken tokenObj = getTokenObject();
            if (tokenObj == null)
            {
                return null;
            }

            var tokenObject = tokenObj.Claims.First(claim => claim.Type == "nameid");
            if (tokenObject == null)
            {
                return null;
            }

            return tokenObject.Value;
        }

        public JwtSecurityToken getTokenObject()
        {
            HttpRequest request = _http.HttpContext.Request;
            var requestHeader = request.Headers[HeaderNames.Authorization];
            if (string.IsNullOrEmpty(requestHeader))
            {
                return null;
            }
            string token = requestHeader.ToString().Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(token);
            return jsonToken as JwtSecurityToken;
        }

        public async Task<UserForReturn> UpdateUser(UserForUpdate user)
        {
            User dbUser = await _context.User.Where(u => u.Username == user.Username).FirstOrDefaultAsync();

            if (dbUser == null) return null;

            dbUser = _mapper.Map<UserForUpdate, User>(user, dbUser);

            if (user.Password != "" && user.Password != null)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

                dbUser.PasswordHash = passwordHash;
                dbUser.PasswordSalt = passwordSalt;
            }

            await _context.SaveChangesAsync();

            UserForReturn retUser = _mapper.Map<UserForReturn>(dbUser);

            return retUser;
        }

        public async Task<PagedList<User>> List(UserParams userParams)
        {
            var userList = _context.User.AsQueryable();
            if (!string.IsNullOrEmpty(userParams.SearchKey))
            {
                userList = userList
                    .Where(c =>
                        // c.regPid.Contains(complaintParams.SearchKey) ||
                        c.PerId.Contains(userParams.SearchKey) ||
                        c.Email.Contains(userParams.SearchKey) ||
                        c.PrefixName.Contains(userParams.SearchKey) ||
                        c.FirstName.Contains(userParams.SearchKey) ||
                        c.LastName.Contains(userParams.SearchKey) ||
                        c.Username.Contains(userParams.SearchKey)
                    );
            }
            if (!string.IsNullOrEmpty(userParams.SearchStatus))
            {
                if (userParams.SearchStatus == "เปิดใช้งาน")
                {
                    userList = userList.Where(c => c.Deleted == 0);
                }
                else if (userParams.SearchStatus == "ปิดใช้งาน")
                {
                    userList = userList.Where(c => c.Deleted == 1);
                }
            }
            userList = userList.OrderByDescending(c => c.Created);
            return await PagedList<User>.CreateAsync(userList, userParams.PageNumber, userParams.PageSize);
        }
    }
}