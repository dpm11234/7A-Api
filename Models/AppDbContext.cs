
using Microsoft.EntityFrameworkCore;

namespace H7A_Api.Models
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Table_Tintuc> Table_Tintuc { get; set; }

  }
}
