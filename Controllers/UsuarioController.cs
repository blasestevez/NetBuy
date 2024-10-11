using LaChozaComercial.Models;
using LaChozaComercial.Models.DTOs;
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
        private readonly UserManager<Usuario> userManager;

        public UsuarioController(IUsuarioRepository usuarioRepository, SignInManager<Usuario> signInManager, UserManager<Usuario> userManager)
        {
            this.usuarioRepository = usuarioRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        // Vista de Registro
        public ActionResult Registro()
        {
            return View();
        }

        // Método de registro con contraseña
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(CreateUsuarioRequestDTO usuarioRequestDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!ChequearEmail(usuarioRequestDTO.email))
                    {
                        ModelState.AddModelError("Email", "El email proporcionado no es válido.");
                        return View(usuarioRequestDTO);
                    }
                    var nuevoUsuario = await usuarioRepository.RegisterUserAsync(usuarioRequestDTO);
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
            return View(usuarioRequestDTO);
        }

        // Vista de Login
        public ActionResult Login()
        {
            return View();
        }

        // Método para manejar el inicio de sesión
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UsuarioDTO usuarioDTO)
        {
            var result = await signInManager.PasswordSignInAsync(usuarioDTO.userName, usuarioDTO.password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                // Ya no necesitas buscar el usuario de nuevo, ya que el SignInManager lo maneja.
                var usuario = await userManager.FindByNameAsync(usuarioDTO.userName);

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

        private bool ChequearEmail(string email)
        {
            try
            {
                var correo = new System.Net.Mail.MailAddress(email);
                return correo.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
