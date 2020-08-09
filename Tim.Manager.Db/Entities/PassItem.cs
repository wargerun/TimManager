using Microsoft.AspNetCore.Identity;
using System;

namespace Tim.Manager.Db.Entities
{
    public class PassItem
    {
        public string UserId { get; set; }
        public string Name { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public Uri Uri { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string Discription { get; set; }
    
        public virtual IdentityUser User { get; set; }
    }
}
