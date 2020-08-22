using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tim.Manager.Db.Entities
{
    public class PassItem
    {
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }

        [Required]
        [StringLength(450, MinimumLength = 8)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string UserName { get; set; }
                
        [Required]
        [MinLength(8)] 
        public string Password { get; set; }

        public Uri Uri { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public string Description { get; set; }
    
        public virtual IdentityUser User { get; set; }
    }
}
