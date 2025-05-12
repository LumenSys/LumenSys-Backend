using Jose;
using LumenSys.Objects.Enums;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace LumenSys.WebAPI.Authentication
{
    public class Token
    {
        [Required(ErrorMessage = "O token é requerido")]
        public string AccessToken { get; private set; }

        public string GenerateToken(string email, TypeEmployee role)
        {
            var security = new TokenSignatures();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(security.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Iss, security.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud, security.Audience),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: security.Issuer,
                audience: security.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetEmailFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
                throw new ArgumentException("Token inválido.", nameof(token));

            var jwt = handler.ReadJwtToken(token);
            return jwt.Claims
                      .FirstOrDefault(c => c.Type == "sub")?.Value
                ?? throw new ArgumentException("Claim 'sub' não encontrada.", nameof(token));
        }

        public bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
                throw new ArgumentException("Token inválido.", nameof(token));

            var jwt = handler.ReadJwtToken(token);
            var exp = jwt.Claims
                         .FirstOrDefault(c => c.Type == "exp")?.Value
                  ?? throw new ArgumentException("Claim 'exp' não encontrada.", nameof(token));

            if (!long.TryParse(exp, out var expUnix))
                throw new ArgumentException("Claim 'exp' inválida.", nameof(token));

            return DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime < DateTime.UtcNow;
        }
    }
}
