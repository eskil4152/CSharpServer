using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class Startup {
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration) {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services){
        var connectionString = configuration["ConnectionString:DefaultConnection"];

        services.AddScoped<PersonFunctions>();
        services.AddScoped<LoginFunctions>();
        services.AddScoped<JwtManagerRepository>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddControllers();

        services.AddCors(options => {
            options.AddPolicy("AllowAnyOrigin",
                builder => {
                    builder.WithOrigins("http://localhost:1234")
                           .AllowAnyHeader()
                           .AllowCredentials()
                           .AllowAnyMethod();
                });
        });

        services.AddAuthentication(x => {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o => {
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = false,
			    ValidateAudience = false,
			    ValidateLifetime = true,
			    ValidateIssuerSigningKey = true,
			    ValidIssuer = configuration["Jwt:Issuer"],
			    ValidAudience = configuration["Jwt:Audience"],
			    IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        services.AddSingleton<IJWTManagerRepository, JwtManagerRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if (env.IsDevelopment()){
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors("AllowAnyOrigin");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}