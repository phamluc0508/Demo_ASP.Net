using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Order");
                e.HasKey(o => o.Id);
                e.Property(o => o.DateOrder).HasDefaultValue(DateTime.Now);
                e.Property(o => o.Status).IsRequired();
                e.Property(o => o.Customer).IsRequired().HasMaxLength(50);
                e.Property(o => o.Address).IsRequired().HasMaxLength(100);
                e.Property(o => o.PhoneNumber).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<OrderDetail>(e =>
            {
                e.ToTable("OrderDetail");
                e.HasKey(o => new {o.OrderId, o.ProductId});
                e.HasOne(o => o.Order).WithMany(o => o.OrderDetails).HasForeignKey(o => o.OrderId).HasConstraintName("FK_OrderDetails_Order");
                e.HasOne(o => o.Product).WithMany(o => o.OrderDetails).HasForeignKey(o => o.ProductId).HasConstraintName("FK_OrderDetails_Product");
                e.Property(o => o.Quantity).IsRequired();
            }
            );
        }
    }
}
