using Microsoft.EntityFrameworkCore;

namespace CompanyTest.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public AppDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=.;Database=CompanyTest;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>(e => 
            {
                e.HasKey(q => q.Id);
                e.Property(q => q.Id).ValueGeneratedNever();
                e.Property(q => q.Name).IsRequired();
            });
        }
    }
}
