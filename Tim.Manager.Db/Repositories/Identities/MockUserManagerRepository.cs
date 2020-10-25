using System.Security.Claims;

namespace Tim.Manager.Db.Repositories.Identities
{
    public class MockUserManagerRepository : IUserManagerRepository
    {
        public bool IsSignedIn(ClaimsPrincipal user)
        {
            return false;
        }
    }
}
