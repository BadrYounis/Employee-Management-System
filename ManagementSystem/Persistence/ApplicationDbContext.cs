using ManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ManagementSystem.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> _options) : DbContext(_options)
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
