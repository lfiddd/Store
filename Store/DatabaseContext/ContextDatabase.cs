using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.DatabaseContext;

public class ContextDatabase : DbContext
{
    public ContextDatabase(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Login> Logins { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<ProductCategories> ProductCategories { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<UserAction> UserAction { get; set; }
    public DbSet<ActionLogs> ActionLogs { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<DeliveryType> DeliveryTypes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique(); 
    }
}