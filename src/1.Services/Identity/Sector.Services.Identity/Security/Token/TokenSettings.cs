using System;
using Microsoft.IdentityModel.Tokens;

namespace NM.Sector.Services.Identity.Security.Token
{
    internal class TokenSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public string TokenPath { get; set; }
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(20);
        public string ApiAccessKey { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
    }
}
