
using Microsoft.EntityFrameworkCore;

namespace H7A_Api.Models
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<TableTintuc> TableTintuc { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<TableTintuc>().ToTable("table_tintuc");
    }

  }
}
