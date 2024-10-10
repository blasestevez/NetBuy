using LaChozaComercial.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LaChozaComercial.Data
{
    public class LaChozaComercialDbContext : IdentityDbContext<IdentityUser>
    {
        public LaChozaComercialDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
