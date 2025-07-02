using BLL.Interfaces;
using BLL.Services;
//using DAL.Utilities;
using Microsoft.EntityFrameworkCore;
using Repository.DataContext;
using Repository.Interfaces;
using Repository.Repositories;

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers();
            //services.AddDbContext<ApplicationDBContext_SQLServer>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddScoped<IPublicacionRepository, PublicacionRepository>();
            //services.AddScoped<IPublicacionService, PublicacionService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();     

        }
    }
}
