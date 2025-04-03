using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LumenSys.WebAPI.Data;

public Startup(IConfiguration configuration)
{
Configuration = configuration;
}

public IConfiguration Configuration { get; }

// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    if (env == "Production")
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
    }
    else
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
    }

    app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
