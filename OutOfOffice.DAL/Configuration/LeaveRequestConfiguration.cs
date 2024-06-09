using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutOfOffice.DAL.Entity;

namespace OutOfOffice.DAL.Configuration;

public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder.HasOne(i => i.Employee)
            .WithMany(i => i.LeaveRequests)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(i => i.AbsenceReason)
            .WithMany(i => i.LeaveRequests)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(i => i.ApprovalRequest)
            .WithOne(i => i.LeaveRequest)
            .HasForeignKey<ApprovalRequest>(a => a.LeaveRequestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}