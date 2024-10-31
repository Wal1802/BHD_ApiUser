
using BHD.Application.Security.Models;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BHD.Application.Security.Authentication
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;
        public JwtFactory(IOptions<JwtIssuerOptions> jwtIssuerOptions)
        {
            _jwtOptions = jwtIssuerOptions.Value;
        }
        public string GenerateEncodedToken(string username)
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, _jwtOptions.IssuetAt.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            };
            
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration.UtcDateTime,
                signingCredentials: _jwtOptions.SigningCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);   
        }
    }
}
