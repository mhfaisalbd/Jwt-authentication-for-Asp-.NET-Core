using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JwtAuthentication.Models
{
    public static class JwtProperties
    {
        public const string AuthScheme = "Identity.Application"+"," + JwtBearerDefaults.AuthenticationScheme;
    }
}