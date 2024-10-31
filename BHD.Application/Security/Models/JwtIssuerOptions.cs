using Microsoft.IdentityModel.Tokens;

namespace BHD.Application.Security.Models
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public DateTimeOffset IssuetAt => DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(30);
        public DateTimeOffset Expiration => IssuetAt.Add(ValidFor);
        public DateTime NotBefore => DateTime.UtcNow;
        public Func<string> JtiGenerator => () => Guid.NewGuid().ToString();
        public SigningCredentials SigningCredentials { get; set; }
    }
}
