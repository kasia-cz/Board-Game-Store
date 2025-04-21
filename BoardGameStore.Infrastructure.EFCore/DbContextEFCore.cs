using BoardGameStore.Infrastructure.EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGameStore.Infrastructure.EFCore
{
    public class DbContextEFCore : DbContext
    {
        public DbSet<BoardGame> BoardGames { get; set; }

        public DbContextEFCore(DbContextOptions<DbContextEFCore> options) : base(options) { }
    }
}
