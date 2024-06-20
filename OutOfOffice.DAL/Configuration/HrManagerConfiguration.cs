using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutOfOffice.DAL.Entity.Employees;

namespace OutOfOffice.DAL.Configuration;

public class HrManagerConfiguration : IEntityTypeConfiguration<HrManager>
{
    public void Configure(EntityTypeBuilder<HrManager> builder)
    {
        builder.HasMany(i => i.Partners)
            .WithOne(i => i.HrManager)
            .HasForeignKey(i => i.HrManagerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}