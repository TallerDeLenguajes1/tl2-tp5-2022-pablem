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
            var pedidoRepo = new RepositorioPedidoSQLite();
            ViewData["idPed"] = pedidoRepo.ProxId();
            /*se recuperan los clientes para construir una select list*/
            var clienteRepo = new RepositorioClienteSQLite();
            var listaClientes = clienteRepo.GetAll();
            /**/
            var altaView = new AltaPedidoViewModel();
            altaView.ListaClientes = new SelectList(listaClientes, "Id", "Nombre");
            altaView.IdCliente = listaClientes.First().Id;
            return View(altaView);
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
            if (id == null) 
                return NotFound();//-------------------<<CONSULTA

            var pedidoRepo = new RepositorioPedidoSQLite();
            var pedidoView = pedidoRepo.GetPedidoYCliente(id);

            if (pedidoView == null)
                return NotFound();

            return View(pedidoView);
        }

        // POST: Cadete/Editar/5
        [HttpPost]
        public IActionResult Editar(ModificarPedidoViewModel pedidoViewModel)
        {
            Pedido pedido = _mapper.Map<Pedido>(pedidoViewModel);
            pedido.Id = pedidoViewModel.IdPedido;///NO se Mapea solo
            Cliente cliente = _mapper.Map<Cliente>(pedidoViewModel);
            cliente.Id = pedidoViewModel.IdCliente;///NO se Mapea solo
            
            var pedidoRepo = new RepositorioPedidoSQLite();
            pedidoRepo.Update(pedido);

            var clienteRepo = new RepositorioClienteSQLite();
            clienteRepo.Update(cliente);

            return RedirectToAction("Index");
        }

        // GET: Pedido/ConfirmacionBorrar/5
        public IActionResult ConfirmacionBorrar(int? id) 
        {
            if (id == null) 
                return NotFound();//-------------------<<CONSULTA
            var pedidoRepo = new RepositorioPedidoSQLite();
            var pedido = pedidoRepo.GetPedido(id);
            if (pedido == null)
                return NotFound();
            BorrarPedidoViewModel borrarView = _mapper.Map<BorrarPedidoViewModel>(pedido);
            return View(borrarView);
        }

        // [HttpGet]
        public IActionResult Borrar(int id) 
        {
            // if (id != null) 
            //     return NotFound();
            var pedidoRepo = new RepositorioPedidoSQLite();
            pedidoRepo.Delete(id);
            return RedirectToAction("Index");
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