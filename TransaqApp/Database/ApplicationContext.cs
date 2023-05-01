using Microsoft.EntityFrameworkCore;

namespace TransaqApp.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Connection> Connections {get;set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=identifier.sqlite");
        }
    }
}

