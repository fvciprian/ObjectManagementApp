using Microsoft.EntityFrameworkCore;
using ObjectManagementApp.Models;

namespace ObjectManagementApp.Data
{
    public class ObjectManagementAppContext : DbContext
    {
        public ObjectManagementAppContext (DbContextOptions<ObjectManagementAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(e => e.CustomObjects)
                .WithMany(e => e.Orders);
        }

        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<CustomObject> CustomObject { get; set; } = default!;
    }
}
