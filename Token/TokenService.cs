using BlogIdentity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogIdentity.Token
{
    public class TokenService : ITokenService
    {
        public TokenService(IConfiguration config)
        {
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.NameId, user.Login)
           };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(14),
            };



            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
