using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RentAPI2.Services;
using RentService;
using RentData;
using Microsoft.EntityFrameworkCore;
using RentAPI2.Helpers;

namespace RentAPI2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddDbContext<RentContext>();
            //(options => options.UseSqlServer("Data Source = Server; Initial Catalog = RentKrok;;Persist Security Info=True;User ID = sa; Password=empty;App=EntityFramework;MultipleActiveResultSets=true"));
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRenterService, RenterService>();
            services.AddScoped<IObjectService, ObjectService>();
            services.AddScoped<ILayerService, LayerService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<ILogService, LogService>();

            services.AddCors(o => o.AddPolicy("ApiPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("ApiPolicy");
            app.UseAuthorization();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
