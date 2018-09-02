using System;
using System.Security.Claims;
using Newtonsoft.Json;

namespace NM.Sector.Services.Identity.Security.Token
{
    public interface IJsonWebTokenFactory
    {
        string GenerateToken(ClaimsIdentity identity, string email, JsonSerializerSettings serializerSettings);
        ClaimsIdentity GenerateClaimsIdentity(Guid id, string email);
    }
}
