
using RentData;
using RentService;

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

            builder.Services.AddDbContext<RentContext>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRenterService, RenterService>();
            builder.Services.AddScoped<IObjectService, ObjectService>();
            builder.Services.AddScoped<ILayerService, LayerService>();
            builder.Services.AddScoped<IAreaService, AreaService>();
            builder.Services.AddScoped<IRateService, RateService>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddScoped<ILogService, LogService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}