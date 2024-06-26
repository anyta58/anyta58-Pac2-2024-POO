using BlogUNAH.API.Database;
using BlogUNAH.API.Services;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogUNAH.API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup( IConfiguration configuration ) 
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //var name = Configuration.GetConnectionString("DefaultConnection");

            //Agragar DbContext

            //services.AddDbContext<BlogUNAHContext>(options => 
            //options.UseSqlServer("Server=localhost;Database=BlogUNAH;User Id=sa;Password=2003; Trusted_Connection=false; TrustServerCertificate=true;"));
            services.AddDbContext<BlogUNAHContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            
            // Add custom service 
            services.AddTransient<ICategoriesService, CategoriesService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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
