using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Settings
{
    public class DatabaseSettings
    {
        public string EventStoreConnectionString { get; set; } = default!;

        public string SqlDbDatabaseName { get; set; } = default!;
    }
}
