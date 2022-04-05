using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Authentication
{
    public class JWTTokenHandler : IJWTTokenHandler
    {
        private readonly string Key;

        public JWTTokenHandler(string key)
        {
            Key = key;
        }

        public string GenerateToken(UserViewModel Model)
        {
           var tokenHandler = new JwtSecurityTokenHandler();
            
            List<Claim> Claims = new List<Claim>();
              Claims.Add(new Claim(ClaimTypes.NameIdentifier, Model.Id.ToString()));
              Claims.Add(new Claim(ClaimTypes.Name, Model.UserName));
              Claims.Add(new Claim(ClaimTypes.Email, Model.Email));
            foreach (var item in Model.UserRoles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, item.RoleName));
            }
           var key = Encoding.ASCII.GetBytes(Key);
           var tokenDescriptor = new SecurityTokenDescriptor
           {
               Subject = new ClaimsIdentity(Claims),
                IssuedAt = System.DateTime.UtcNow,
                Expires = System.DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
           };
           var token = tokenHandler.CreateToken(tokenDescriptor);
           return tokenHandler.WriteToken(token);
        }
    }
}