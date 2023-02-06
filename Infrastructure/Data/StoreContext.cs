using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        // options includes the connection string which is set in: appsettings.Development.json
        public StoreContext(DbContextOptions options) : base(options)
        {
            
        }   

        // Tell data context about our entity and bring in using API.Entities
        public DbSet<Product> Products { get; set; } //Entity Framework has "Products" as the name of the table
    }
}