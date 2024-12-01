using BaseApplication.Domains;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication.Repository.Data.Entity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(x => x.FirstName)
                .HasMaxLength(50);
            builder.Property(x => x.LastName)
                .HasMaxLength(50);

            builder
               .HasIndex(x => new { x.Email })
               .IsUnique();

            builder
                .Property(x => x.Email)
                .HasMaxLength(50);

            builder
                .Property(x => x.Address)
                .IsRequired(false)
                .HasMaxLength(150);
            builder.Property(x => x.ContactNumber)
                .HasMaxLength(15);
            builder.Property(x => x.Zipcode)
                .HasMaxLength(20);
            builder
               .HasOne(x => x.ApplicationUser)
               .WithMany()
               .HasForeignKey(x => x.AspNetUserId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.Cascade);        }
    }
}
