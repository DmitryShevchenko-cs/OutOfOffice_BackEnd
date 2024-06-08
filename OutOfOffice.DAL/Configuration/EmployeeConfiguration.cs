using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutOfOffice.DAL.Entity;
using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.DAL.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasIndex(r => r.Id);
        
        builder.HasOne(r => r.Subdivision)
            .WithMany(r => r.Employees)
            .HasForeignKey(r => r.SubdivisionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(r => r.Position)
            .WithMany(r => r.Employees)
            .HasForeignKey(r => r.PositionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(r => r.HrManager)
            .WithMany(r => r.Partners)
            .HasForeignKey(r => r.PositionId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(e => e.OutOfOfficeBalance)
            .HasColumnType("decimal(18,2)");
    }
}