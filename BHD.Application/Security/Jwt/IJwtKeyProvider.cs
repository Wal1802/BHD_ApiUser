using Microsoft.IdentityModel.Tokens;


namespace BHD.Application.Security.Jwt
{
    public interface IJwtKeyProvider
    {
        TokenValidationParameters GetValidationParameters();
        SigningCredentials GetSigningCredentials();
    }
}
