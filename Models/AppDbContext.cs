using Microsoft.EntityFrameworkCore;

namespace POSInventory.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItem> BillItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Category
            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryId);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Inventories)
                .WithOne(i => i.Category)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Category>()
                .HasQueryFilter(c => !c.IsDeleted);

            // Inventory
            modelBuilder.Entity<Inventory>()
                .HasKey(i => i.InventoryId);
            modelBuilder.Entity<Inventory>()
                .HasQueryFilter(i => !i.IsDeleted);

            // Bill
            modelBuilder.Entity<Bill>()
                .HasKey(b => b.BillId);
            modelBuilder.Entity<Bill>()
                .HasMany(b => b.Items)
                .WithOne(i => i.Bill)
                .HasForeignKey(i => i.BillId);
            modelBuilder.Entity<BillItem>()
                .HasKey(i => i.BillItemId);
        }
    }
}
