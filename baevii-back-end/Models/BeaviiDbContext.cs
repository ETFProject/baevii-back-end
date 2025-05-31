using Microsoft.EntityFrameworkCore;

namespace baevii_back_end.Models;

public class BeaviiDbContext : DbContext
{
    public DbSet<WalletInfo> walletInfos {  get; set; }
    public DbSet<User> users { get; set; }
    public DbSet<Account> accounts { get; set; }
    public DbSet<ServerWallet> serverWallets { get; set; }
    
    public BeaviiDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
