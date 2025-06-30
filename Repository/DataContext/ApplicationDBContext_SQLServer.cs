using DAL.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.IdentityEF;

namespace Repository.DataContext
{
    public class ApplicationDBContext_SQLServer : IdentityDbContext<CustomIdentityUser>
    {
        public ApplicationDBContext_SQLServer(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
    }
}