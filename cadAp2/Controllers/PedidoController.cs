using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using ViewModels;
using Repositorios;

namespace cadAp2.Controllers
{
    public class PedidoController : Controller
    {
        static int numeroPedidos;
        public static List<Pedido> listaPedidos = new List<Pedido>();
        private readonly IMapper _mapper;
        private readonly ILogger<PedidoController> _logger;

        public PedidoController(ILogger<PedidoController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var pedidoRepo = new RepositorioPedidoSQLite();
            var listaViewPedidos = pedidoRepo.GetAll();
            return View(listaViewPedidos);
        }

        public IActionResult Alta()
        {
            ViewData["idPed"] = numeroPedidos+1;//cambiar
            /*se recuperan los clientes para construir una select list*/
            var clienteRepo = new RepositorioClienteSQLite();
            var listaClientes = clienteRepo.GetAll();
            /**/
            var altaView = new AltaPedidoViewModel();
            altaView.ListaClientes = new SelectList(listaClientes, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public IActionResult GuardarPedido(AltaPedidoViewModel pedidoViewModel) 
        {
            var pedidoRepo = new RepositorioPedidoSQLite();
            pedidoRepo.Save(pedidoViewModel);
            return RedirectToAction("Index");
        }

        // GET: Pedido/Editar/5
        public IActionResult Editar(int? id)
        {
            ViewData["idPed"] = id;
            
            if (id == null) 
                return NotFound();

            var pedido = listaPedidos.Single(x => x.Id == id);
            if (pedido == null)
                return NotFound();

            ModificarPedidoViewModel editarView = _mapper.Map<ModificarPedidoViewModel>(pedido);
            return View(editarView);
        }

        // POST: Cadete/Editar/5
        [HttpPost]
        public IActionResult Editar(ModificarPedidoViewModel pedidoViewModel)
        {
            Pedido actual = _mapper.Map<Pedido>(pedidoViewModel);
            Pedido anterior = listaPedidos.Single(x => x.Id == actual.Id);
            anterior.Detalles = actual.Detalles;
            anterior.Estado = actual.Estado;//sorry not sorry --> TP6: UPDATE
            return RedirectToAction("Index",listaPedidos);
        }

        // GET: Pedido/ConfirmacionBorrar/5
        public IActionResult ConfirmacionBorrar(int id) 
        {
            var pedido = listaPedidos.Single(x => x.Id == id); //.SingleOrDefault(); 
            BorrarPedidoViewModel borrarView = _mapper.Map<BorrarPedidoViewModel>(pedido);
            return View(borrarView);
        }

        // [HttpGet]
        public IActionResult Borrar(int id) 
        {
            listaPedidos.Remove(listaPedidos.Single(x => x.Id == id));
            return RedirectToAction("Index",listaPedidos);
        }

        // GET: Cadete/AsignarCadete/5
        public IActionResult AsignarCadete(int id)
        {
            ViewData["idPed"] = id;
            // List<AsignarCadeteViewModel> asignarView = _mapper.Map<List<AsignarCadeteViewModel>>(CadeteController.listaCadetes);
            var asignarView = new AsignarCadeteViewModel();
            asignarView.Lista = new SelectList(CadeteController.listaCadetes, "Id", "Nombre");
            return View(asignarView);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}