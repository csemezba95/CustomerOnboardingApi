using Microsoft.EntityFrameworkCore;
using CustomerOnboardingApi.Models;

namespace CustomerOnboardingApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<OtpVerification> OtpVerifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.ICNumber)
                .IsUnique();

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.Name).HasColumnType("varchar(100)");
                entity.Property(c => c.ICNumber).HasColumnType("BIGINT");
                entity.Property(c => c.MobileNumber).HasColumnType("varchar(15)");
                entity.Property(c => c.Email).HasColumnType("varchar(100)");
                entity.Property(c => c.Pin).HasColumnType("int");
            });

            modelBuilder.Entity<OtpVerification>(entity =>
            {
                entity.Property(c => c.MobileNumber).HasColumnType("varchar(15)");
                entity.Property(o => o.Email).HasColumnType("varchar(100)");
                entity.Property(o => o.OtpCode).HasColumnType("int");
            });
        }
    }
}
