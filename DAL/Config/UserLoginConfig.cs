using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Config {
    public class UserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>> {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder) {
            builder.ToTable("UserLogin");
        }
    }
}
