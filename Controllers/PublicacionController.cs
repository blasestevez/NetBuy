using AutoMapper;
using LaChozaComercial.Models;
using LaChozaComercial.Models.DTOs;
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
        private readonly IMapper mapper;

        public PublicacionController(IPublicacionRepository publicacionRepository, UserManager<Usuario> userManager, IMapper mapper)
        {
            this.publicacionRepository = publicacionRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // Mostrar las Publicaciones del Vendedor logeado
        public async Task<IActionResult> MisPublicaciones()
        {
            // Llama a la funcion del repositorio buscando las publicaciones con el id del usuario logeado mediante UserManager
            var publicaciones = await publicacionRepository.GetMisPublicacionesAsync(userManager.GetUserId(User));
            return View(publicaciones);
        }

        public IActionResult CrearPublicacion()
        {
            return View();
        }

        // Crear una publicación tomando como parametro los datos enviados por el formulario
        [HttpPost]
        public async Task<IActionResult> CrearPublicacion(Publicacion publicacion)
        {
            // Añade el id del vendedor a la publicacion mediante el UserManager
            publicacion.usuarioId = userManager.GetUserId(User);

            // Mapeo de la publicacion a DTO para interactuar con el repositorio
            var createPublicacionDTO = mapper.Map<CreatePublicacionRequestDTO>(publicacion);
            await publicacionRepository.CreatePublicacionAsync(createPublicacionDTO);

            // Luego de crear la publicacion redirecciona a la vista MisPublicaciones
            return RedirectToAction("MisPublicaciones");
        }

        // Muestra las publicaciones de todos los vendedores
        public async Task<IActionResult> VerPublicaciones()
        {
            // Llama a la funcion del repositorio obteniendo todas las publicaciones
            var publicaciones = await publicacionRepository.GetPublicacionesAsync();
            return View(publicaciones);
        }
    }
}
