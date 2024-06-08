using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutOfOffice.DAL.Entity;

namespace OutOfOffice.DAL.Configuration;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasMany(i => i.Employees)
            .WithMany(i => i.Projects);

        builder.HasOne(i => i.ProjectType)
            .WithMany(i => i.Projects)
            .OnDelete(DeleteBehavior.Restrict);
    }
}