using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infra.Identity
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> roles)
        {
            roles.ToTable("roles");
            roles.HasData(new IdentityRole { Name = Roles.Padrao, NormalizedName = Roles.Padrao.ToUpper() });
        }
    }
}