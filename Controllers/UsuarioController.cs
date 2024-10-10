using LaChozaComercial.Models;
using LaChozaComercial.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LaChozaComercial.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly SignInManager<Usuario> signInManager;

        public UsuarioController(IUsuarioRepository usuarioRepository, SignInManager<Usuario> signInManager)
        {
            this.usuarioRepository = usuarioRepository;
            this.signInManager = signInManager;
        }

        // Vista de Registro
        public ActionResult Registro()
        {
            return View();
        }

        // Método de registro con contraseña
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(Usuario usuario, string contraseña)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var nuevoUsuario = await usuarioRepository.RegisterUserAsync(usuario, contraseña);
                    if (nuevoUsuario != null)
                    {
                        return RedirectToAction("Login");
                    }
                    ModelState.AddModelError(string.Empty, "Error al registrar el usuario");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(usuario);
        }

        // Vista de Login
        public ActionResult Login()
        {
            return View();
        }

        // Método para manejar el inicio de sesión
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string nombreUsuario, string contraseña)
        {
            var result = await signInManager.PasswordSignInAsync(nombreUsuario, contraseña, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                // Ya no necesitas buscar el usuario de nuevo, ya que el SignInManager lo maneja.
                var usuario = await usuarioRepository.LoginUserAsync(nombreUsuario, contraseña);

                if (usuario != null)
                {
                    // Redireccionar según el tipo de usuario
                    if (usuario.UserType) // Asumiendo que UserType es un bool
                    {
                        return RedirectToAction("MisPublicaciones", "Publicacion"); // Vendedor
                    }
                    else
                    {
                        return RedirectToAction("VerPublicaciones", "Publicacion"); // Cliente
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
            return View();
        }

        // Método de logout
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
