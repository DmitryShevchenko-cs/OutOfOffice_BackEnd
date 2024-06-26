using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutOfOffice.DAL.Entity;

namespace OutOfOffice.DAL.Configuration;

public class ApprovalRequestConfiguration : IEntityTypeConfiguration<ApprovalRequest>
{
    public void Configure(EntityTypeBuilder<ApprovalRequest> builder)
    {
        
        builder.HasOne(ar => ar.LeaveRequest)
            .WithOne(lr => lr.ApprovalRequest)
            .HasForeignKey<ApprovalRequest>(ar => ar.LeaveRequestId);

        builder.HasOne(i => i.Approver)
            .WithMany()
            .HasForeignKey(a => a.ApproverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}