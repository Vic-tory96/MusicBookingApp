using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                   new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                   new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                   new Claim(JwtRegisteredClaimNames.Email, user.Email),
                   new Claim("name", user.UserName), 
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60), // 1 hour expiry
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //public string GenerateToken(ApplicationUser user)
        //{
        //    var secretKey = _configuration["JwtSettings:SecretKey"];
        //    var issuer = _configuration["JwtSettings:Issuer"];
        //    var audience = _configuration["JwtSettings:Audience"];
        //    var expiryMinutes = Convert.ToInt32(_configuration["JwtSettings:ExpiryMinutes"]);

        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: issuer,
        //        audience: audience,
        //        expires: DateTime.Now.AddMinutes(expiryMinutes),
        //        signingCredentials: credentials
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }

}
