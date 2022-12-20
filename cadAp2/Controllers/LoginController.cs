using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Repositorios;
using ViewModels;

namespace cadAp2.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        public IRepositorioUsuario _repoUser;

        public LoginController(ILogger<LoginController> logger, IRepositorioUsuario repoUser)
        {
            _repoUser = repoUser;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var loginView = new LoginViewModel();
            return View(loginView);
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel loginView)
        {
            var usuarios = _repoUser.GetAll(); 
            if(usuarios == null || !usuarios.Any())
                return NotFound();
            var usuario = usuarios.SingleOrDefault(x => x.NikName == loginView.NikName && x.Password == loginView.Password, null);
            if(usuario != null)
            {
                HttpContext.Session.SetString("user", usuario.NikName);
                HttpContext.Session.SetString("rol", usuario.Rol.ToString());
                return RedirectToAction("Index","Home");
            } else {
                TempData["mensaje"] = "Nombre de usuario y/o contraseña incorrectos";
                return View(loginView);
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}