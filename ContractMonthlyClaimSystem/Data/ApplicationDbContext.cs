using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContractMonthlyClaimSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<ClaimApplication>()
                .HasOne(c => c.Status)
                .WithMany()
                .HasForeignKey(c => c.StatusId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Lecturer> Lecturers { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Designation> Designations { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<SystemCode> SystemCodes { get; set; }

        public DbSet<SystemCodeDetail> SystemCodeDetails { get; set; }

        public DbSet<ClaimType> ClaimTypes { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<ClaimApplication> ClaimApplications { get; set; }
    }

}
