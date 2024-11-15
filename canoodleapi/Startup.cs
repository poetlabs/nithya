using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using canoodleapi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace canoodleapi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JsonOptions>(options =>
            {

                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllers().AddJsonOptions(
               options =>
               {
                   options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                   options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
               });

            services.Configure<AppSettings>(options => Configuration.GetSection("Connections").Bind(options));
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<ILaborerRepository, LaborerRepository>();
            services.AddTransient<IMachineRepository, MachineRepository>();
            services.AddTransient<IRouteRepository, RouteRepository>();
            services.AddTransient<IMaintenanceActivityRepository, MaintenanceActivityRepository>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMemoryCache();
            services.AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings
            .NullValueHandling = Newtonsoft.Json.NullValueHandling.Include);
            services.AddMvc().AddViewLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc().AddMvcOptions(options => options.EnableEndpointRouting = false);
            string[] origins = Convert.ToString(Configuration.GetSection("TokenAuthentication:Origin").Value).Split(',');

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(origins)
                     .AllowAnyMethod()
                     .AllowAnyHeader());
                options.AddPolicy("AllowAll",
                   builder => builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod());
            });

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });


        }
    }
}