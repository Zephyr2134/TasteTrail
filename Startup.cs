using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.DataAbstraction;
using EPiServer.DataAbstraction.RuntimeModel;
using EPiServer.DependencyInjection;
using EPiServer.Scheduler;
using EPiServer.Web.Routing;

namespace TasteTrail
{
    public class Startup(IWebHostEnvironment webHostingEnvironment)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            if (webHostingEnvironment.IsDevelopment())
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(webHostingEnvironment.ContentRootPath, "App_Data"));

                services.Configure<SchedulerOptions>(options => options.Enabled = false);
            }

            services
                .AddCmsAspNetIdentity<ApplicationUser>()
                .AddCms()
                .AddAdminUserRegistration()
                .AddEmbeddedLocalization<Startup>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapContent();

                // Temporary diagnostic, remove when done
                if (env.IsDevelopment())
                {
                    endpoints.MapGet("/diag/contenttypes", (
                        IConfiguration configuration,
                        ContentTypeModelRepository modelRepository,
                        IContentTypeRepository contentTypeRepository) =>
                    {
                        string? connectionString = configuration.GetConnectionString("EPiServerDB");

                        var scannedModels = modelRepository
                            .List()
                            .Select(m => m.ModelType.FullName ?? m.Name)
                            .OrderBy(name => name)
                            .ToList();

                        var typesInDatabase = contentTypeRepository
                            .List()
                            .Select(t => $"{t.Name} ({t.GUID}) -> {t.ModelType?.FullName ?? "no model"}")
                            .OrderBy(name => name)
                            .ToList();

                        var loadedAssemblies = AppDomain.CurrentDomain
                            .GetAssemblies()
                            .Select(a => a.GetName().Name ?? string.Empty)
                            .Where(name => !name.StartsWith("System") && !name.StartsWith("Microsoft"))
                            .OrderBy(name => name)
                            .ToList();

                        return Results.Json(new
                        {
                            Environment = env.EnvironmentName,
                            DataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory"),
                            ConnectionString = connectionString,
                            ScannedModels = scannedModels,
                            TypesInDatabase = typesInDatabase,
                            LoadedAssemblies = loadedAssemblies
                        }, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                    });
                }
            });
        }
    }
}