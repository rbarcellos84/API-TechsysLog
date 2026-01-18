using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TechsysLog.Web.Api.Security
{
    public static class JwtSecurity
    {
        public static string GenerateToken(string email, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Busca a chave do JSON centralizado
            var secretKey = configuration["JwtSettings:Secret"];
            var key = Encoding.ASCII.GetBytes(secretKey);
            var expirationHours = double.Parse(configuration["JwtSettings:ExpirationHours"] ?? "8");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, email),
                    // O SignalR usa o NameIdentifier para mapear usuários aos Hubs
                    new Claim(ClaimTypes.NameIdentifier, email)
                }),
                Expires = DateTime.UtcNow.AddHours(expirationHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}