using Microsoft.EntityFrameworkCore;

public class Startup
{
    private readonly IConfiguration config;

    public Startup(IConfiguration configuration)
    {
        config = configuration;
    }

    public void ConfigureServices(IServiceCollection services){
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddScoped<Person>();
        services.AddScoped<PersonFunctions>();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if (env.IsDevelopment()){
            app.UseDeveloperExceptionPage();
        } else {
            // Configure error handling, logging, and other production-specific settings here...
        }
        
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}