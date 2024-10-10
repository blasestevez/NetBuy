using LaChozaComercial.Models;
using LaChozaComercial.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LaChozaComercial.Controllers
{
    public class PublicacionController : Controller
    {
        private readonly IPublicacionRepository publicacionRepository;
        private readonly UserManager<Usuario> userManager;

        public PublicacionController(IPublicacionRepository publicacionRepository, UserManager<Usuario> userManager)
        {
            this.publicacionRepository = publicacionRepository;
            this.userManager = userManager;
        }

        // Acción para mostrar las publicaciones del usuario autenticado
        public async Task<IActionResult> MisPublicaciones()
        {
            var publicaciones = await publicacionRepository.GetMisPublicacionesAsync(userManager.GetUserId(User));
            return View(publicaciones);
        }

        // Vista para crear una nueva publicación
        public IActionResult CrearPublicacion()
        {
            return View();
        }

        // Acción para manejar la creación de una nueva publicación
        [HttpPost]
        public async Task<IActionResult> CrearPublicacion(Publicacion publicacion)
        {
            // Agregar el nombre del vendedor a la publicación
            publicacion.usuarioId = userManager.GetUserId(User);
            await publicacionRepository.CreatePublicacionAsync(publicacion);
            return RedirectToAction("MisPublicaciones");
        }

        // Acción para mostrar todas las publicaciones en la base de datos
        public async Task<IActionResult> VerPublicaciones()
        {
            var publicaciones = await publicacionRepository.GetPublicacionesAsync();
            return View(publicaciones);
        }
    }
}
