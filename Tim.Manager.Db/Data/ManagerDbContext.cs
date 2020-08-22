using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tim.Manager.Db.Data.Configurations;
using Tim.Manager.Db.Entities;

namespace Tim.Manager.Db.Data
{
    public class ManagerDbContext : IdentityDbContext
    {
        public virtual DbSet<PassItem> PassItems { get; set; }

        public ManagerDbContext(DbContextOptions<ManagerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new PassItemConfiguration());
        }
    }
}
