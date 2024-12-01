using BaseApplication.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApplication.Repository.Data.Entity
{
    public class RoleScreenActionConfiguration : IEntityTypeConfiguration<RoleScreenAction>
    {
        public void Configure(EntityTypeBuilder<RoleScreenAction> builder)
        {
            builder
               .HasOne(x => x.ScreenRole)
               .WithMany()
               .HasForeignKey(x => x.ScreenRoleId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.NoAction);

            builder
              .HasOne(x => x.ScreenAction)
              .WithMany()
              .HasForeignKey(x => x.ScreenActionId)
              .IsRequired(true)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
