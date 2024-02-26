using Microsoft.EntityFrameworkCore;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }
}
