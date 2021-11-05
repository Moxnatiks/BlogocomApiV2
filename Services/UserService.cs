using BlogocomApiV2.Exceptions;
using BlogocomApiV2.GraphQl.Users;
using BlogocomApiV2.Interfaces;
using BlogocomApiV2.Models;
using BlogocomApiV2.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace BlogocomApiV2.Services
{
    public class UserService : BaseAuth
    {
        private readonly IUser _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUser userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private string Role = "user";

        protected override string getRole()
        {
            return Role;
        }

        public TokenResponse UserLogin (string phone, string password)
        {
            if (password == null) throw new ArgumentException("Password is missing!");
            if (phone == null) throw new ArgumentException("Number phone is missing!");


            var identity = GetIdentity(phone, password);

                if (identity != null)
                {
                    var token = CreateToken(identity);

                    var response = new TokenResponse()
                    {
                        Role = Role,
                        Token = token.ToString()
                    };

                    return response;

                }
                else throw new ArgumentException("Invalid email or password!");

           
        }



        protected override string checkPerson(string phone, string password)
        {
            //User user = db.Users.FirstOrDefault(x => x.Phone == phone);
            User user = _userRepository.FindOfDefaultUserByPhone(phone);
            if (user != null)
            {

                bool isPasswordMatched = new HashPassword(password).VerifyPassword(user.StoredSalt, user.Password);
                if (isPasswordMatched)
                {
                    if (user.IsAccess)
                    {
                        return user.Id.ToString();
                    }
                    else throw new AppException("Access blocked!");
                }
                return null;
            }
            return null;
            /*else
            {
                HashSalt data = new HashPassword(password).EncryptPassword();
                User newUser = new User
                {
                    Phone = phone,
                    StoredSalt = data.Salt,
                    Password = data.Hash,
                    IsAccess = true
                };

                _userRepository.AddUserAsync(newUser);
                //db.Users.Add(newUser);
                //db.SaveChanges();
                return newUser.Id.ToString();
            }*/
        }

        public bool ValidateToken(string inputToken)
        {
            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var user = handler.ValidateToken(inputToken,
                    new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = AuthSettings.AUDIENCE,
                        ValidIssuer = AuthSettings.ISSUER,
                        RequireSignedTokens = false,
                        IssuerSigningKey = AuthSettings.GetSymmetricSecurityKey(),
                    },
                    out SecurityToken token);

                if (user.Identity.IsAuthenticated)
                {
                    _httpContextAccessor.HttpContext.User = user;
                    return true;
                }
            }
            catch { }
            return false;
            
            
           
        }

        public long GetUserId()
        {
            long userId = Convert.ToInt64(_httpContextAccessor.HttpContext.User.Identity.Name);
            return userId;
        }

    }
}
