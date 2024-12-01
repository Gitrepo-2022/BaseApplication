using BaseApplication.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApplication.Repository.Data.Entity
{
    public class ScreenRoleConfiguration : IEntityTypeConfiguration<ScreenRole>
    {
        public void Configure(EntityTypeBuilder<ScreenRole> builder)
        {
            builder
               .HasOne(x => x.Screen)
               .WithMany()
               .HasForeignKey(x => x.ScreenId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);

            builder
              .HasOne(x => x.Role)
              .WithMany()
              .HasForeignKey(x => x.RoleId)
              .IsRequired(true)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
