using Microsoft.EntityFrameworkCore;
using Storage_Management_Application.Models;

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

        public DbSet<UnitsOM> UnitsOMs { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<ReceiptResource> ReceiptResources { get; set; }
        public DbSet<ShipmentResource> ShipmentResources { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ReceiptDocument> ReceiptDocuments { get; set; }
        public DbSet<ShipmentDocument> ShipmentDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка отношений между сущностями
            modelBuilder.Entity<Balance>()
                .HasOne(b => b.Resource)
                .WithMany(r => r.Balances)
                .HasForeignKey(b => b.ResourceId);
            modelBuilder.Entity<Balance>()
                .HasOne(b => b.UnitsOM)
                .WithMany(u => u.Balances)
                .HasForeignKey(b => b.UnitsOMId);
            modelBuilder.Entity<ReceiptResource>()
                .HasOne(rr => rr.Resource)
                .WithMany(r => r.ReceiptResources)
                .HasForeignKey(rr => rr.ResourceId);
            modelBuilder.Entity<ReceiptResource>()
                .HasOne(rr => rr.UnitsOM)
                .WithMany(u => u.ReceiptResources)
                .HasForeignKey(rr => rr.UnitsOMId);
            modelBuilder.Entity<ShipmentResource>()
                .HasOne(sr => sr.Resource)
                .WithMany(r => r.ShipmentResources)
                .HasForeignKey(sr => sr.ResourceId);
            modelBuilder.Entity<ShipmentResource>()
                .HasOne(sr => sr.UnitsOM)
                .WithMany(u => u.ShipmentResources)
                .HasForeignKey(sr => sr.UnitsOMId);
        }
    }
}
