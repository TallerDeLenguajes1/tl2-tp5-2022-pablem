using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cadAp2.Models;

namespace cadAp2.Controllers
{
    public class CadeteController : Controller
    {
        static int numeroCadetes = 0;
        static List<Cadete> listaCadetes = new List<Cadete>();

        private readonly ILogger<CadeteController> _logger;

        public CadeteController(ILogger<CadeteController> logger)
        {
            _logger = logger;
        }

        /***** //spoiler alert: implementar con base de datos
        public async Task<IActionResult> Index()
        {
            return _context.Movie != null ? 
                View(await _context.Movie.ToListAsync()) :
                Problem("Entity set 'MvcMovieContext.Movie'  is null.");
        }
        *******/

        public IActionResult Index()
        {
            return View(listaCadetes);
        }

        public IActionResult AltaCadete()
        {
            ViewData["idCad"] = numeroCadetes+1;
            return View();
        }

        // [HttpGet]
        // public IActionResult ActualizarCadete(int id)
        // {
        //     return View(listaCadetes.Select(x => x.Id == id).Single());
        // }

        [HttpPost]
        public IActionResult GuardarCadete(Cadete cadete) {
            cadete.Id = ++numeroCadetes;
            listaCadetes.Add(cadete);
            return RedirectToAction("Index",listaCadetes);
        }

        public IActionResult ConfirmacionBorrar(int id) 
        {
            ///////control if(listaCadetes.any)
            var cadete = listaCadetes.Single(x => x.Id == id); //.SingleOrDefault(); 
            return View(cadete);
        }

        // [HttpGet]
        public IActionResult BorrarCadete(int id) {
            listaCadetes.Remove(listaCadetes.Single(x => x.Id == id));
            return RedirectToAction("Index",listaCadetes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}