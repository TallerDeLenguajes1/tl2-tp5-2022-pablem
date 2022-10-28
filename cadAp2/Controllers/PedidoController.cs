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
    // [Route("[controller]")]
    public class PedidoController : Controller
    {
        static int numeroPedidos;

        static List<Pedido> listaPedidos = new List<Pedido>();

        private readonly ILogger<PedidoController> _logger;

        public PedidoController(ILogger<PedidoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(listaPedidos);
        }

        public IActionResult AltaPedido()
        {
            ViewData["idPed"] = numeroPedidos+1;
            return View();
        }

        [HttpPost]
        public IActionResult GuardarCadete(Pedido pedido) {
            pedido.Id = ++numeroPedidos;
            listaPedidos.Add(pedido);
            return RedirectToAction("Index",listaPedidos);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}