
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RentData;
using RentService;
using System.Configuration;
using System.Text;

namespace RentApi3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

			IConfigurationRoot configuration = new ConfigurationBuilder()
    		  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
		        .AddJsonFile("appsettings.json")
		            .Build();
			builder.Services.AddDbContext<RentContext>(options => options.UseSqlServer(
			configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRenterService, RenterService>();
            builder.Services.AddScoped<IObjectService, ObjectService>();
            builder.Services.AddScoped<ILayerService, LayerService>();
            builder.Services.AddScoped<IAreaService, AreaService>();
            builder.Services.AddScoped<IRateService, RateService>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddScoped<ILogService, LogService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}