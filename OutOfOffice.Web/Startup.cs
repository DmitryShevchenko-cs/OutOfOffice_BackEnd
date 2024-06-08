using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using OutOfOffice.DAL;


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
        services.AddControllers().AddNewtonsoftJson(opt => 
            opt.SerializerSettings.Converters.Add(new StringEnumConverter()));
        
        var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING") ?? Configuration.GetConnectionString("ConnectionString");
        
                
        
        services.AddDbContext<OfficeDbContext>(options =>
            options.UseSqlServer(connectionString));
        
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
    }
}