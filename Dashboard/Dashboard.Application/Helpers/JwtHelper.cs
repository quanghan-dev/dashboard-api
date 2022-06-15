using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Dashboard.Application.Helpers
{
    public class JwtHelper
    {
        private readonly byte[] _key;
        private TokenValidationParameters _tokenValidationParameters;
        public JwtHelper(byte[] key, TokenValidationParameters tokenValidationParameters)
        {
            _key = key;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public string GenerateAccessToken(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id)
                }),
                Expires = DateTime.UtcNow.AddHours(6),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
