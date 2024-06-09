using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
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
        
        // DI
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        
        
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