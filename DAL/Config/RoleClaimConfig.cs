using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Config {
    public class RoleClaimConfig : IEntityTypeConfiguration<IdentityRoleClaim<string>> {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder) {
            builder.ToTable("RoleClaim");
        }
    }
}
