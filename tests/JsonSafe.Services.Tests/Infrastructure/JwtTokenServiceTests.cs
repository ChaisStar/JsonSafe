namespace JsonSafe.Services.Tests.Infrastructure
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using AutoFixture.NUnit3;
    using Microsoft.IdentityModel.Tokens;
    using Models;
    using NSubstitute;
    using NUnit.Framework;
    using Services.Infrastructure;

    [TestFixture]
    public class JwtTokenServiceTests
    {
        [Test, AutoData]
        public void CreateToken_Should_Create_Correct_Token(UserModel userModel, string issuer, string key, int lifetime)
        {
            var jwtConfig = Substitute.For<IJwtTokenConfig>();
            jwtConfig.Issuer.Returns(issuer);
            jwtConfig.Key.Returns(key);
            jwtConfig.Lifetime.Returns(lifetime);
            var token = new JwtTokenService(jwtConfig).CreateToken(userModel);
            var handler = new JwtSecurityTokenHandler();
            var result = handler.ReadJwtToken(token);

            handler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidAudience = issuer,
                    ValidIssuer = issuer
                },
                out var validatedToken);
            Assert.NotNull(validatedToken);

            Assert.AreEqual(issuer, result.Issuer);
            Assert.AreEqual(issuer, result.Audiences.FirstOrDefault());
            Assert.That(DateTime.UtcNow.AddMinutes(lifetime), Is.EqualTo(result.ValidTo).Within(2).Seconds);
            Assert.AreEqual(SecurityAlgorithms.HmacSha256, result.SignatureAlgorithm);
            Assert.AreEqual(userModel.Id.ToString(), result.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            Assert.AreEqual(userModel.Username, result.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value);
        }
    }
}
