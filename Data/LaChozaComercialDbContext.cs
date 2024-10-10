using LaChozaComercial.Models;
using Microsoft.EntityFrameworkCore;

namespace LaChozaComercial.Data
{
    public class LaChozaComercialDbContext : DbContext
    {
        public LaChozaComercialDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
