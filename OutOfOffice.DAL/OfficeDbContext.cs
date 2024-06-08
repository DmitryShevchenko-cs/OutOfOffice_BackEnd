using Microsoft.EntityFrameworkCore;
using OutOfOffice.DAL.Entity;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.DAL;

public class OfficeDbContext : DbContext
{
    public OfficeDbContext(DbContextOptions<OfficeDbContext> options) : base(options)
    {
    }
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<HrManager> HrManagers { get; set; }
    public DbSet<ProjectManager> ProjectManagers { get; set; }
    
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<ApprovalRequest> ApprovalRequests { get; set; }

    public DbSet<AbsenceReason> AbsenceReasons { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<ProjectType> ProjectTypes { get; set; }
    public DbSet<Subdivision> Subdivisions { get; set; }

    public DbSet<Project> Projects { get; set; }
    public DbSet<BaseManagerEntity> Managers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OfficeDbContext).Assembly);
    }
}