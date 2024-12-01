using BaseApplication.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApplication.Repository.Data.Entity
{
    public class ScreenActionConfiguration:IEntityTypeConfiguration<ScreenAction>
    {
        public void Configure(EntityTypeBuilder<ScreenAction> builder)
        {
            builder
               .HasOne(x => x.Screen)
               .WithMany()
               .HasForeignKey(x => x.ScreenId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
