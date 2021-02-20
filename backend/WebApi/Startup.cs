using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace WebApi
{

    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var config = Configuration.GetSection("configuration").Get<Configuration>();

            var env = config.Dev;
            var envName = "dev";

            if (Environment.IsEnvironment("Production"))
            {
                env = config.Prd;
                envName = "prd";
            }

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={Environment.ContentRootPath}\\{env.SqlliteDbName};")
                );

            services.AddCors(options =>
                        options.AddDefaultPolicy(builder =>
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                        )
                    );

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.Configure<ConfigEnvironment>(Configuration.GetSection("configuration:" + envName));
            services.AddMvc().AddNewtonsoftJson();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "ArmNas", Version = "v1" }));
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app) // , IWebHostEnvironment env
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArmNas"));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

    }

}
