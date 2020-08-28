using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tim.Manager.Db.Entities
{
    public class PassItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(450)]
        public string UserId { get; set; }

        [Required]
        [MaxLength(450)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
                
        [Required]
        public string Password { get; set; }

        public Uri Uri { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public string Description { get; set; }
                           
        public virtual IdentityUser User { get; set; }
    }
}
