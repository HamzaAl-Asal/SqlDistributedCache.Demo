using Microsoft.EntityFrameworkCore;
using SqlDistributedCache.Demo.DataAccess.Entities;

namespace SqlDistributedCache.Demo.DataAccess
{
    public class SqlDistributedCacheAppContext : DbContext
    {
        private readonly IConfiguration configuration;

        public SqlDistributedCacheAppContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<CachedItems> CachedItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlDistributedCacheAppContext"));
            }
        }
    }
}