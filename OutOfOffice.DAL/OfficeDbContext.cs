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

    public DbSet<BaseEmployeeEntity> BaseEmployees { get; set; }

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

    public DbSet<AuthorizationInfo> AuthorizationInfos { get; set; }

    public DbSet<Admin> Admins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>().HasData(
            new Admin
            {
                Id = 1,
                Login = "admin",
                Password = "AMnqUjltBJxY6WDypk7qmQ4ftQh1k+IqhlM/FBD7jvhadR3QJVo9EzherHLrK70AQw==", // hashed password
                FullName = "ADMIN",
                isDeactivated = false,
            });

        // Seed AbsenceReason Data
        modelBuilder.Entity<AbsenceReason>().HasData(
            new AbsenceReason { Id = 1, ReasonDescription = "Sick" },
            new AbsenceReason { Id = 2, ReasonDescription = "Vacation" }
        );

        // Seed Position Data
        modelBuilder.Entity<Position>().HasData(
            new Position { Id = 1, Name = "Software Engineer" },
            new Position { Id = 2, Name = "Frontend Developer" },
            new Position { Id = 3, Name = "Backend Developer" },
            new Position { Id = 4, Name = "Full Stack Developer" },
            new Position { Id = 5, Name = "QA Engineer" },
            new Position { Id = 6, Name = "UI/UX Designer" }
        );

        // Seed ProjectType Data
        modelBuilder.Entity<ProjectType>().HasData(
            new ProjectType { Id = 1, Name = "Web App" },
            new ProjectType { Id = 2, Name = "Data Migration" },
            new ProjectType { Id = 3, Name = "Cloud Computing Project" },
            new ProjectType { Id = 4, Name = "Blockchain" }
        );

        // Seed Subdivision Data
        modelBuilder.Entity<Subdivision>().HasData(
            new Subdivision { Id = 1, Name = "Development" },
            new Subdivision { Id = 2, Name = "Operations" },
            new Subdivision { Id = 3, Name = "Support" },
            new Subdivision { Id = 4, Name = "Security" },
            new Subdivision { Id = 5, Name = "QA" },
            new Subdivision { Id = 6, Name = "Data" }
        );
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OfficeDbContext).Assembly);
    }
}