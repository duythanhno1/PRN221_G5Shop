using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Config {
    public class RoleConfig : IEntityTypeConfiguration<IdentityRole> {
        public void Configure(EntityTypeBuilder<IdentityRole> builder) {
            builder.ToTable("Role");
            builder.HasData(
                  new IdentityRole {
                      Name = "ShopOwner",
                      NormalizedName = "SHOPOWNER"
                  }, new IdentityRole {
                      Name = "Customer",
                      NormalizedName = "CUSTOMER"
                  }, new IdentityRole {
                      Name = "Administrator",
                      NormalizedName = "ADMINISTRATOR"
                  });
        }
    }
}
