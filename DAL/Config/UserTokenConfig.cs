using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Config {
    public class UserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>> {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder) {
            builder.ToTable("UserToken");
        }
    }
}
