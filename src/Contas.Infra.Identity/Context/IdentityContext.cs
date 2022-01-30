using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infra.Identity
{
    public class IdentityContext : IdentityDbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("identity");

            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new IdentityRoleClaimConfiguration());
            builder.ApplyConfiguration(new IdentityRoleConfiguration());
            builder.ApplyConfiguration(new IdentityUserClaimConfiguration());
            builder.ApplyConfiguration(new IdentityUserConfiguration());
            builder.ApplyConfiguration(new IdentityUserLoginConfiguration());
            builder.ApplyConfiguration(new IdentityUserRoleConfiguration());
            builder.ApplyConfiguration(new IdentityUserTokenConfiguration());
        }
    }
}
