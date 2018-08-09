namespace JsonSafe.Services.Infrastructure
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using Models;

    public class JwtTokenService : IJwtTokenService
    {
        private readonly IJwtTokenConfig _jwtTokenConfig;

        public JwtTokenService(IJwtTokenConfig jwtTokenConfig)
        {
            _jwtTokenConfig = jwtTokenConfig;
        }

        public string CreateToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_jwtTokenConfig.Issuer,
                _jwtTokenConfig.Issuer,
                expires: DateTime.Now.AddMinutes(_jwtTokenConfig.Lifetime),
                signingCredentials: credentials,
                claims: new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                });

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}