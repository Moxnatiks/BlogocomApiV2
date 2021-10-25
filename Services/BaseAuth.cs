using BlogocomApiV2.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogocomApiV2.Services
{
    public abstract class BaseAuth
    {
        protected abstract string getRole();
        protected abstract string checkPerson(string unique, string password);
        protected ClaimsIdentity GetIdentity(string unique, string password)
        {
            string userId = checkPerson(unique, password);
            if (userId != null)
            {
                var claims = new List<Claim>
            {
                new Claim("id", userId),
                new Claim(ClaimsIdentity.DefaultNameClaimType, userId),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,  getRole()),
                new Claim(ClaimsIdentity.DefaultIssuer,  userId),

            };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            // если пользователя не найдено
            return null;
        }
        protected object CreateToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthSettings.ISSUER,
                    audience: AuthSettings.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthSettings.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthSettings.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
