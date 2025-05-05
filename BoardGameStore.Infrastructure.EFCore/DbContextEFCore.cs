using BoardGameStore.Infrastructure.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGameStore.Infrastructure.EFCore
{
    public class DbContextEFCore : DbContext
    {
        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbContextEFCore(DbContextOptions<DbContextEFCore> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(i => i.BoardGame)
                .WithMany()
                .HasForeignKey(i => i.BoardGameId);
        }
    }
}
