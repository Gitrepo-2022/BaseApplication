using BaseApplication.Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication.Repository.Data
{
    public class BaseApplicationDbContext : IdentityDbContext<ApplicationUser, AppIdentityRole, int>
    {
        public BaseApplicationDbContext(DbContextOptions<BaseApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<ScreenAction> ScreenActions { get; set; }
        public DbSet<ScreenRole> ScreenRoles { get; set; }
        public DbSet<RoleScreenAction> RoleScreenActions { get; set; }
        public DbSet<Tenant> Tenant { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            _ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseApplicationDbContext).Assembly);
           modelBuilder.Entity<ApplicationUser>().HasQueryFilter(b => EF.Property<int>(b, "TenantId") == b.TenantId);
        }
    }
}
