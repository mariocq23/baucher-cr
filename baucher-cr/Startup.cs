using data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;

namespace baucher_api {
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<FacturaRepository>(); // Register the repository as a singleton
            services.AddSingleton<OrdenRepository>(); // Register the repository as a singleton
            services.AddSingleton<ClienteRepository>(); // Register the repository as a singleton
            services.AddSingleton<EmpleadoRepository>(); // Register the repository as a singleton
            services.AddSingleton<ProductoRepository>(); // Register the repository as a singleton
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Baucher API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Baucher v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}