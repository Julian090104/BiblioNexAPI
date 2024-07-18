using Business.implementations;
using Business.implementations.Business.Implementations;
using Business.Implementations;
using Business.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BiblioNexAPI
{
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BiblioNexAPI", Version = "v1" });
            });

            // Register the IUsuarioServices with its implementation UsuarioServices
            services.AddTransient<IUsuarioServices, UsuarioServices>();
            services.AddTransient<ILibroServices, LibroServices>();
            services.AddTransient<IPrestamoServices, PrestamoServices>();

            // Register the DbContext with SQL Server
            services.AddDbContext<BiblioNexContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DevConnection")),
                ServiceLifetime.Scoped);

            // Allow CORS from any origin
            services.AddCors(options => options.AddPolicy("AllowWebApp", builder =>
                builder.AllowAnyHeader()
                       .AllowAnyOrigin()
                       .AllowAnyMethod()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BiblioNexAPI v1"));
            }

            // Allow CORS from any origin
            app.UseCors("AllowWebApp");

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

