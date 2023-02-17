using Microsoft.EntityFrameworkCore;
using SoftTeK.BusinessAdvisors.Data.Entities;

namespace SoftTeK.BusinessAdvisors.Data.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            // Make sure the database is created, else do it
            this.Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
    }
}
