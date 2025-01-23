using DAL.Model.Publicacion;
using Microsoft.EntityFrameworkCore;

namespace Repository.DataContext
{
    public class ApplicationDBContext_SQLServer : DbContext
    {
        public ApplicationDBContext_SQLServer(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Publicacion> Publicaciones { get; set; }
    }
}
