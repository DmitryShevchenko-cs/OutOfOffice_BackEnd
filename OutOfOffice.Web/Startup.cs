using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using OutOfOffice.BLL.Services;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL;
using OutOfOffice.DAL.Repository;
using OutOfOffice.DAL.Repository.Interfaces;


namespace OutOfOffice.Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
 
    public void ConfigureServices(IServiceCollection services)
    { 
        //JSON serialization and add a converter for enums
        services.AddControllers().AddNewtonsoftJson(opt => 
            opt.SerializerSettings.Converters.Add(new StringEnumConverter()));
        
        //dbConnection
        var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING") ?? Configuration.GetConnectionString("ConnectionString");
        services.AddDbContext<OfficeDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        //automapper
        services.AddAutoMapper(typeof(Startup));
        
        // DI Repositories
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IApprovalRequestRepository, ApprovalRequestRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        
        // DI Services
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IApprovalRequestService, ApprovalRequestService>();
        services.AddScoped<ILeaveRequestService, LeaveRequestService>();
        services.AddScoped<IManagerService, ManagerService>();
        services.AddScoped<IProjectService, ProjectService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        app.UseRouting();
        app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
    }
}