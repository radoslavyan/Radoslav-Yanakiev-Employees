using Microsoft.EntityFrameworkCore;
using PairOfEmployees.Domain.Models;

namespace PairOfEmployees.Infrastructure.DatabaseContext
{
    public class PairDbContext : DbContext
    {
        public PairDbContext(DbContextOptions<PairDbContext> options) : base(options) { }
        public DbSet<PairOfEmployeesData> PairsOfEmployeesData { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PairOfEmployeesData>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.FileName).HasMaxLength(256);
            });
        }
    }
}
