using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NM.SharedKernel.Common.Claims;
using NM.SharedKernel.Infrastructure.Guards;

namespace NM.Sector.Services.Identity.Infrastructure.Token
{
    internal class JsonWebTokenFactory : IJsonWebTokenFactory
    {
        #region Fields

        private readonly TokenSettings _tokenSettings;


        #endregion

        #region Constructor

        public JsonWebTokenFactory(IOptions<TokenSettings> tokenSettingsOptions)
        {
            _tokenSettings = tokenSettingsOptions.Value;
        }

        #endregion

        #region Methods

        public string GenerateToken(ClaimsIdentity identity, string email, JsonSerializerSettings serializerSettings)
        {
            var token = new
            {
                id = identity.Claims.Single(claim => claim.Type == CustomClaimTypes.Id).Value,
                auth_token = GenerateEncodedToken(email, identity),
                expires_in = (int)_tokenSettings.ValidFor.TotalSeconds
            };
            return JsonConvert.SerializeObject(token, serializerSettings);
        }

        public ClaimsIdentity GenerateClaimsIdentity(Guid id, string email)
        {
            Guards.GuidCannotBeNullOrEmpty(id, nameof(id));
            Guards.StringCannotBeNullWhiteSpaceOrEmpty(email, nameof(email));

            return new ClaimsIdentity(new GenericIdentity(email, "Token"), new[]
            {
                new Claim(CustomClaimTypes.Id, id.ToString()),
                new Claim(CustomClaimTypes.ApiAccess, _tokenSettings.ApiAccessKey)
            });
        }

        private string GenerateEncodedToken(string email, ClaimsIdentity identity)
        {
            var claims = new[]
            {
                identity.FindFirst(CustomClaimTypes.Id),
                identity.FindFirst(CustomClaimTypes.ApiAccess),
                new Claim(CustomClaimTypes.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenSettings.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64)
            };

            var token = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                claims: claims,
                notBefore: _tokenSettings.NotBefore,
                expires: _tokenSettings.Expiration,
                signingCredentials: _tokenSettings.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalMilliseconds);

        #endregion
    }
}
