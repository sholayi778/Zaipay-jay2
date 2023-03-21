using Microsoft.EntityFrameworkCore;
using Zaipay.Models;

namespace Zaipay
{
    public class ZaipayDbContext : DbContext
    {
        public ZaipayDbContext(DbContextOptions<ZaipayDbContext> options) : base(options) { }

        public DbSet<TokenModel> Tokens { get; set; }
        public DbSet<ZaiAccount> ZaiAccounts { get; set; }
        public DbSet<TransactionRecord> TransactionRecords { get; set; }

    }
}
