using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.DAL.Configuration;

public class BaseManagerConfiguration : IEntityTypeConfiguration<BaseManagerEntity>
{
    public void Configure(EntityTypeBuilder<BaseManagerEntity> builder)
    {
        builder.HasMany(i => i.ApprovalRequest)
            .WithOne(i => i.Approver)
            .OnDelete(DeleteBehavior.Restrict);
    }
}