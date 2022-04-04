using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace api.Utils
{
    public class TokenUtil
    {
        public static JwtSecurityToken getTokenObject(HttpRequest request)
        {
            var requestHeader = request.Headers[HeaderNames.Authorization];
            if (string.IsNullOrEmpty(requestHeader)) {
                return null;
            }
            string token = requestHeader.ToString().Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(token);
            return jsonToken as JwtSecurityToken;
        }

        public static string GetUserNameFromToken(HttpRequest request)
        {
            if (request == null)
                return null;

            JwtSecurityToken tokenObj = getTokenObject(request);
            if (tokenObj == null) {
                return null;
            }

            var tokenObject = tokenObj.Claims.First(claim => claim.Type == "nameid");
            if (tokenObject == null) {
                return null;
            }

            return tokenObject.Value;
        }

        public static string GetRoleFromToken(HttpRequest request)
        {
            if (request == null)
                return null;

            JwtSecurityToken tokenObj = getTokenObject(request);
            if (tokenObj == null) {
                return null;
            }

            var tokenObject = tokenObj.Claims.First(claim => claim.Type == "role");
            if (tokenObject == null) {
                return null;
            }

            return tokenObject.Value;
        }

    }
}