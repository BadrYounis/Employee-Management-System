using ManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementSystem.Persistence.EntitiesConfigurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasData(
                new Department
                {
                    Id = 1,
                    Name = "HR",
                    Description = "Responsible for recruitment, employee relations",
                },
                new Department
                {
                    Id = 2,
                    Name = "Information Technology",
                    Description = "Manages software development, infrastructure, security, and technical support",
                },
                new Department
                {
                    Id = 3,
                    Name = "Finance",
                    Description = "Handles accounting, financial planning, and company expenses",
                }
            );
        }
    }
}