using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using OData.Swagger.Services;
using WebApi.Models.Internal;

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

        /// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain.</exception>
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
            services.AddControllers().AddNewtonsoftJson();
            services.AddOData(opt =>
                opt.AddModel("odata", GetEdmModel())
                    .Select()
                    .Expand()
                    .Filter()
                    .Count()
                    .OrderBy()
                );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ArmNas", Version = "v1" });
                c.DocInclusionPredicate((_, api) => api.HttpMethod != null); // oData fix
            });
            services.AddOdataSwaggerSupport();
        }

        public void Configure(IApplicationBuilder app) // , IWebHostEnvironment env
        {
            InitializeDatabase(app);
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArmNas"));
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            Debug.Assert(scope != null, nameof(scope) + " != null");
            scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EnableLowerCamelCase();
            builder.EntitySet<Partition>("Partition");
            builder.EntitySet<Message>("Message");
            builder.EntitySet<AppHistory>("AppHistory");
            return builder.GetEdmModel();
        }

    }

}
