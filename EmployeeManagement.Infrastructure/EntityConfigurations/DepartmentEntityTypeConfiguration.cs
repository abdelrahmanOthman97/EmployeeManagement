using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Infrastructure.EntityConfigurations
{
    public class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(200);

            //---------- Relation ----------
            builder.Property(e => e.ManagerId);
            builder.HasOne<Employee>(d => d.Manager)
               .WithOne(e => e.DepartmentManager)
               .HasForeignKey<Department>(d => d.ManagerId)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
