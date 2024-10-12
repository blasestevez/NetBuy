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

        public ActionResult Registro()
        {
            return View();
        }

        // Registra un usuario obteniendo los datos del formulario de la vista de registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(CreateUsuarioRequestDTO usuarioRequestDTO)
        {
            // Si el modelo enviado por el formulario es valido
            if (ModelState.IsValid)
            {
                try
                {
                    // Verifica que el mail ingresado no sea válido
                    if (!ChequearEmail(usuarioRequestDTO.email))
                    {
                        ModelState.AddModelError("Email", "El email proporcionado no es válido.");
                        return View(usuarioRequestDTO);
                    }
                    // Si es válido se le envia el DTO del usuario a registar al repositorio
                    var nuevoUsuario = await usuarioRepository.RegisterUserAsync(usuarioRequestDTO);

                    // Si el registro fue exitoso se redirecciona a la vista de Login
                    if (nuevoUsuario != null)
                    {
                        return RedirectToAction("Login");
                    }
                    // Si hubo un error en el registro se muestra un error
                    ModelState.AddModelError(string.Empty, "Error al registrar el usuario");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(usuarioRequestDTO);
        }

        public ActionResult Login()
        {
            return View();
        }

        // Inicio de sesión
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UsuarioDTO usuarioDTO)
        {
            // Se le envia el DTO recibido del formulario al repositorio
            var result = await signInManager.PasswordSignInAsync(usuarioDTO.userName, usuarioDTO.password, isPersistent: false, lockoutOnFailure: false);
            
            // Si el inicio de sesión fue exitoso
            if (result.Succeeded)
            {
                // Se busca el nombre del usuario con el UserManager
                var usuario = await userManager.FindByNameAsync(usuarioDTO.userName);

                // Si el usuario existe
                if (usuario != null)
                {
                    // Redirecciona a la vista segun el tipo del usuario
                    if (usuario.UserType)
                    {
                        return RedirectToAction("MisPublicaciones", "Publicacion"); // Vista de vendedor
                    }
                    else
                    {
                        return RedirectToAction("VerPublicaciones", "Publicacion"); // Vista de cliente
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
            return View();
        }

        // Cerrar sesión
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        // Función para chequear si el email es válido
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
