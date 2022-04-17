using System.Security.Claims;
using System.Security.Principal;

namespace Backend.Utils
{
    public class ClaimHelper
    {
        public static Claim? GetClaim(IIdentity? claimsIdentity, string claimTypes)
        {
            if (claimsIdentity == null)
            {
                return null;
            }

            List<Claim> claims = ((ClaimsIdentity)claimsIdentity).Claims.ToList();
            return claims.Find(element => element.Type == claimTypes);
        }
    }
}