using Microsoft.EntityFrameworkCore;

namespace baevii_back_end.Models;

public class BeaviiDbContext : DbContext
{
    public DbSet<WalletInfo> walletInfos {  get; set; }

    public BeaviiDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
