using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Config {
    public class UserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>> {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder) {
            builder.ToTable("UserRole");
        }
    }
}
