namespace JsonSafe.WebApi.Extensions
{
    using System;
    using System.Linq;
    using System.Security.Claims;

    public static class UserExtensions
    {
        public static string GetUsername(this ClaimsPrincipal @this)
        {
            return @this.Identity.Name;
        }

        public static Guid GetUserId(this ClaimsPrincipal @this)
        {
            return new Guid(@this.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
