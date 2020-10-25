using System.Security.Claims;

namespace Tim.Manager.Db.Repositories.Identities
{
    public interface IUserManagerRepository
    {
        public bool IsSignedIn(ClaimsPrincipal user);
    }
}
