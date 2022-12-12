using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using RentData;
using RentCommon;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Security.Cryptography;

namespace RentService
{

    public class UserService : IUserService
    {

        private readonly RentContext _rm;

        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration, RentContext rm)
        {
        
             _configuration = configuration;
            _rm = rm;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            //var pwd = PwdCrypt.Decrypt(model.Password);
            //var user = _rm.Users.SingleOrDefault(x=>x.Username == model.Username);
            //if (user == null) return null;

            //if (PwdCrypt.Decrypt(user.Password) == PwdCrypt.Decrypt(model.Password))
            //{
            //    var token = generateJwtToken(user);
            //    return new AuthenticateResponse(user, token);
            //}
            //else 
                return null;

        }

        public User GetById(int id)
        {
            return _rm.Users.FirstOrDefault(x => x.Id == id);
        }

        // helper methods
        private string generateJwtToken(User user)
        {
            //// generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("name", user.Username) }),
                Expires = DateTime.UtcNow.AddDays(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

            //List<Claim> claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.Id.ToString()) //,
            //   // new Claim(ClaimTypes.Role, "Admin")
            //};

            //var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            //    _configuration.GetSection("AppSettings:Secret").Value));

            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //var token = new JwtSecurityToken(
            //    claims: claims,
            //    expires: DateTime.Now.AddDays(1),
            //    signingCredentials: creds);

            //var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            //return jwt;



        }
  
        public List<User> ListUsers()
        {
            return _rm.Users.ToList();
        }
        public bool NewUser(User user)
        {
            //try {
            //    user.Password = PwdCrypt.Encrypt(user.Password);
            //    _rm.Users.Add(user);
            //    _rm.SaveChanges();
            //    return true;
            //}
            //catch
            //{
                return false;
            //}
        }

        public bool EditUser(User user)
        {
            //try
            //{
            //    var u = _rm.Users.Where(x=>x.Id == user.Id).First();
            //    u.LastName = user.LastName;
            //    u.FirstName = user.FirstName;
            //    u.Email = user.Email;
            //    u.Password = PwdCrypt.Encrypt(user.Password);
            //    _rm.SaveChanges();
            //    return true;
            //}
            //catch
            //{
                return false;
            //}
        }

        public bool DeleteUser(int id)
        {
            try
            {
                var u = _rm.Users.Where(x => x.Id == id).FirstOrDefault();
                _rm.Users.Remove(u);
                _rm.SaveChanges();
                return true;
            }
            catch
                {
                return false;
            }
        }


		private string CreateToken(User user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Role, "Admin")
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
				_configuration.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}


		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(passwordHash);
			}
		}

	}
}