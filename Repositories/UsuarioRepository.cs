using LaChozaComercial.Data;
using LaChozaComercial.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LaChozaComercial.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly LaChozaComercialDbContext dbContext;
        private readonly UserManager<Usuario> userManager;

        public UsuarioRepository(LaChozaComercialDbContext dbContext, UserManager<Usuario> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        // Registro de un nuevo usuario con UserManager y DbContext
        public async Task<Usuario> RegisterUserAsync(Usuario usuario, string contraseña)
        {
            // Crear el usuario usando el UserManager
            var result = await userManager.CreateAsync(usuario, contraseña);

            // Verificar si la creación fue exitosa
            if (result.Succeeded)
            {
                // No necesitas agregar el usuario de nuevo al DbContext
                // return usuario; // Puedes retornar el usuario aquí si lo necesitas
                return usuario; // Ya está agregado al DbContext
            }

            // Lanzar una excepción si hubo errores
            throw new Exception("Error al registrar el usuario: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }


        // Iniciar sesión y devolver el usuario si la autenticación es correcta
        public async Task<Usuario> LoginUserAsync(string nombreUsuario, string contraseña)
        {
            var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.UserName == nombreUsuario);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(usuario, contraseña);
            if (!isPasswordValid)
            {
                throw new Exception("Contraseña incorrecta");
            }

            return usuario;
        }
    }
}
