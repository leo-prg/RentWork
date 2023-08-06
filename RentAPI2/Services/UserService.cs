using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using RentDB;
using RentAPI2.Helpers;
using RentAPI2.Models;
using Microsoft.AspNetCore.Mvc;
using RentCommon;

namespace RentAPI2.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User GetById(int id);
        List<User> ListUsers();
        bool NewUser(User user);
        bool EditUser(User user);
        bool DeleteUser(int id);
    }

    public class UserService : IUserService
    {

        private static readonly RentModel rm = new RentModel();

        private List<User> _users = rm.Users.ToList();

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.Username);
            // return null if user not found
            //&& x.Password == model.Password


            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    // authentication successful so generate jwt token
                    var token = generateJwtToken(user);
                    return new AuthenticateResponse(user, token);
                }
                else {
                    return null;
                }
            }
            else return null;

        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
  
        public List<User> ListUsers()
        {
            return rm.Users.ToList();
        }
        public bool NewUser(User user)
        {
            try {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); //PwdCrypt.Encrypt(user.Password);
                rm.Users.Add(user);
                rm.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditUser(User user)
        {
            try
            {
                var u = rm.Users.Where(x=>x.Id == user.Id).First();
                u.LastName = user.LastName;
                u.FirstName = user.FirstName;
                u.Email = user.Email;
                u.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);//PwdCrypt.Encrypt(user.Password);
                rm.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                var u = rm.Users.Where(x => x.Id == id).FirstOrDefault();
                rm.Users.Remove(u);
                rm.SaveChanges();
                return true;
            }
            catch
                {
                return false;
            }
        }
      
    }
}