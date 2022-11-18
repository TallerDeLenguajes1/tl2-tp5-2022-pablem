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
        private readonly ILogger<PedidoController> _logger;
        private readonly IMapper _mapper;
        private readonly IRepositorioCadete _repoCad;
        private readonly IRepositorioPedido _repoPed;
        private readonly IRepositorioCliente _repoCli;

        public PedidoController(ILogger<PedidoController> logger, IMapper mapper, IRepositorioCadete repoCad, IRepositorioPedido repoPed, IRepositorioCliente repoCli)
        {
            _logger = logger;
            _mapper = mapper;
            _repoCad = repoCad;
            _repoPed = repoPed;
            _repoCli = repoCli;
        }

        public IActionResult Index()
        {
            var listaViewPedidos = _repoPed.GetAll();
            return View(listaViewPedidos);
        }

        public IActionResult Alta()
        {
            ViewData["idPed"] = _repoPed.ProxId();
            /*se recuperan los clientes para construir una select list*/
            var listaClientes = _repoCli.GetAll();
            var altaView = new AltaPedidoViewModel();
            altaView.ListaClientes = new SelectList(listaClientes, "Id", "Nombre");
            if(listaClientes != null && listaClientes.Any())
                altaView.IdCliente = listaClientes.First().Id;
            return View(altaView);
        }

        [HttpPost]
        public IActionResult GuardarPedido(AltaPedidoViewModel pedidoViewModel) 
        {
            _repoPed.Save(pedidoViewModel);
            return RedirectToAction("Index");
        }

        // GET: Pedido/Editar/5
        public IActionResult Editar(int? id)
        {
            if (id == null) 
                return NotFound();//-------------------<<CONSULTA

            var pedidoView = _repoPed.GetPedidoYCliente(id);

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
            // var pedidoRepo = new RepositorioPedidoSQLite();
            _repoPed.Update(pedido);
            _repoCli.Update(cliente);
            return RedirectToAction("Index");
        }

        // GET: Pedido/ConfirmacionBorrar/5
        public IActionResult ConfirmacionBorrar(int? id) 
        {
            if (id == null) 
                return NotFound();//-------------------<<CONSULTA
            var pedido = _repoPed.GetById(id);
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
            _repoPed.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Pedido/AsignarCadete/5
        public IActionResult AsignarCadete(int id)
        {
            var asignarView = new AsignarCadeteViewModel();
            asignarView.IdPedido = id;
            /*creo una select list con cadetes*/
            var listaCadetes = _repoCad.GetAll();
            asignarView.Cadetes = new SelectList(listaCadetes, "Id", "Nombre");
            /*Obtengo el cadete actual, si lo hubiera*/
            asignarView.IdCadete = _repoPed.ObtenerCadeteId(id);
            return View(asignarView);
        }

        [HttpPost]
        public IActionResult AsignarCadete(AsignarCadeteViewModel asignarView)
        {
            _repoPed.AsignarCadete(asignarView);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}