using Microsoft.EntityFrameworkCore;

namespace Storage_Management_Application.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Автоматическое создание базы данных, если она не существует
            Database.EnsureCreated();

            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }
    }
}
