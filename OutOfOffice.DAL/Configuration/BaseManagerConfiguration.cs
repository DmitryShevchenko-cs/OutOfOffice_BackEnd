using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutOfOffice.DAL.Entity;
using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.DAL.Configuration;

public class BaseManagerConfiguration : IEntityTypeConfiguration<BaseManagerEntity>
{
    public void Configure(EntityTypeBuilder<BaseManagerEntity> builder)
    {
        builder.HasMany(i => i.ApprovalRequests)
            .WithOne(i => i.Approver)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(i => i.AuthorizationInfo)
            .WithOne(i => (BaseManagerEntity)i.Employee)
            .HasForeignKey<AuthorizationInfo>(i => i.EmployeeId);
    }
}