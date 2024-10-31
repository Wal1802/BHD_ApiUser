using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BHD.Application.Security.Jwt
{
    public class JwtKeyProvider : IJwtKeyProvider
    {
        private readonly string KEY_FILE_NAME = ".jwt.key";
        private readonly int KEY_SIZE = 128;
        private readonly string _issuer;
        private readonly string _audience;
        private static SymmetricSecurityKey _securityKey;
        public JwtKeyProvider(string issuer, string audience)
        {
            _issuer = issuer;
            _audience = audience;
        }
        public SigningCredentials GetSigningCredentials()
        {
            var _signingKey = GetSecurityKey();
            return new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
        }

        public TokenValidationParameters GetValidationParameters()
        {
            var _signingKey = GetSecurityKey();

            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer,

                ValidateAudience = true,
                ValidAudience = _audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,


                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }
        private SymmetricSecurityKey GetSecurityKey()
        {
            if (_securityKey == null)
                _securityKey = new SymmetricSecurityKey(GetSecretKey());

            return _securityKey;
        }
        private byte[] GetSecretKey()
        {
            string path = Path.Combine(AppContext.BaseDirectory, KEY_FILE_NAME);

            if (File.Exists(path))
                return File.ReadAllBytes(path);

            var key = new byte[KEY_SIZE];
            RandomNumberGenerator.Fill(key);
            File.WriteAllBytes(path, key);
            File.SetAttributes(path, FileAttributes.Hidden);

            return key;
        }
    }
}
