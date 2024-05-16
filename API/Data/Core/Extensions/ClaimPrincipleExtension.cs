using System.Security.Claims;

namespace DatingApp.API.Data.Core.Extensions
{
    public static class ClaimPrincipleExtension
    {
        public static string GetUserName(this ClaimsPrincipal principal) => 
            principal.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}
