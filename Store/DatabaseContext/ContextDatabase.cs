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
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<ProductCategories> ProductCategories { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Session> Sessions { get; set; }
}