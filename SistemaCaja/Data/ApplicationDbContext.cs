using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SistemaCaja.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("dbo");
            builder.Entity<IdentityUser>(e => { e.ToTable(name: "NetCoreUsers", schema: "CajaWeb"); });
            builder.Entity<IdentityRoleClaim<string>>(e => { e.ToTable(name: "NetCoreRoleClaims", schema: "CajaWeb"); });
            builder.Entity<IdentityRole>(e => { e.ToTable(name: "NetCoreRoles", schema: "CajaWeb"); });
            builder.Entity<IdentityUserClaim<string>>(e => { e.ToTable(name: "NetCoreUserClaims", schema: "CajaWeb"); });
            builder.Entity<IdentityUserLogin<string>>(e => { e.ToTable(name: "NetCoreUserLogins", schema: "CajaWeb"); });
            builder.Entity<IdentityUserToken<string>>(e => { e.ToTable(name: "NetCoreUserTokens", schema: "CajaWeb"); });
            builder.Entity<IdentityUserRole<string>>(e => { e.ToTable(name: "NetCoreUserRoles", schema: "CajaWeb"); });

        }

    }
}
