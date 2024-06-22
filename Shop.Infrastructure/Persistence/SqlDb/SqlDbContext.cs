using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shop.Application.ReadModels;
using Shop.Infrastructure.Settings;


namespace Shop.Infrastructure.Persistence.SqlDb
{
    public class SqlDbContext : DbContext
    {
        private readonly IOptions<DatabaseSettings> _databaseSettings;

        public DbSet<CustomerReadModel> Customers { get; set; }

        public DbSet<ProductReadModel> Products { get; set; }

        public SqlDbContext(IOptions<DatabaseSettings> databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(_databaseSettings.Value.SqlDbDatabaseName);
        }
    }
}
