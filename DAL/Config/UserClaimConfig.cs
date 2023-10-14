using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Config {
    public class UserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<string>> {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder) {
            builder.ToTable("UserClaim");
        }
    }
}
