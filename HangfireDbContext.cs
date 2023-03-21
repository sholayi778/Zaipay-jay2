using Microsoft.EntityFrameworkCore;

namespace Zaipay
{
    //public class HangfireDbContext
    //{
    //}
    public class HangfireDbContext : DbContext
    {
        public HangfireDbContext(DbContextOptions<HangfireDbContext> options) : base(options)
        {

        }
    }
}
