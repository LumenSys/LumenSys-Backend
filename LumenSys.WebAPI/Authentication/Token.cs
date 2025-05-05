using Jose;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace LumenSys.WebAPI.Authentication
{
    public class Token
    {
        [Required(ErrorMessage = "O token é requerido")]
        public string AccessToken { get; private set; }

        public string GenerateToken(string email)
        {
            var security = new TokenSignatures();
            var payload = new Dictionary<string, object>
            {
                { "iss", security.Issuer },
                { "aud", security.Audience },
                { "sub", email },
                { "exp", DateTimeOffset.UtcNow.AddMinutes(60).ToUnixTimeSeconds() }
            };

            return AccessToken = JWT.Encode(payload, Encoding.UTF8.GetBytes(security.Key), JwsAlgorithm.HS256);
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
