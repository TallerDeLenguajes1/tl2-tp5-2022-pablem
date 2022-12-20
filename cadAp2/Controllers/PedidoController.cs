using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using ViewModels;
using Repositorios;

namespace cadAp2.Controllers
{
    public class PedidoController : ControllerConValidacionAcceso
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IMapper _mapper;
        private readonly IRepositorioCadete _repoCad;
        private readonly IRepositorioPedido _repoPed;
        private readonly IRepositorioCliente _repoCli;

        public PedidoController(ILogger<PedidoController> logger, 
                                IMapper mapper, 
                                IRepositorioCadete repoCad, 
                                IRepositorioPedido repoPed, 
                                IRepositorioCliente repoCli)
        {
            _logger = logger;
            _mapper = mapper;
            _repoCad = repoCad;
            _repoPed = repoPed;
            _repoCli = repoCli;
        }

        public IActionResult Index()
        {
            if (AccionSinAcceso() != null)
                return AccionSinAcceso();

            var pedidos = _repoPed.GetAll();
            var pedidosView = _mapper.Map<List<MostrarPedidoViewModel>>(pedidos);
            foreach (var pedView in pedidosView)
            {
                Cadete cad = _repoPed.ObtenerCadete(pedView.Id);
                pedView.NombreCadete = (cad != null) ? cad.Nombre : "";
            }
            return View(pedidosView);
        }

        public IActionResult Alta()
        {
            if (AccionAccesoRestringido() != null)
                return AccionAccesoRestringido();

            ViewData["idPed"] = _repoPed.GetLastId()+1;
            /*se recuperan los clientes para construir una select list item*/
            var listaClientes = _repoCli.GetAll();
            var altaView = new AltaPedidoViewModel();
            altaView.ListaClientes = new SelectList(listaClientes, "Id", "Nombre");
            if(listaClientes != null && listaClientes.Any())
                altaView.IdCliente = listaClientes.First().Id;
            return View(altaView);
        }

        [HttpPost]
        public IActionResult GuardarPedido(AltaPedidoViewModel pedidoView) 
        {
            if (AccionAccesoRestringido() != null)
                return AccionAccesoRestringido();

            var idCliente = pedidoView.IdCliente;
            var nuevoPedido = _mapper.Map<Pedido>(pedidoView);
            _repoPed.Save(nuevoPedido, idCliente);
            return RedirectToAction("Index");
        }

        // GET: Pedido/Editar/5
        public IActionResult Editar(int? id)
        {
            if (AccionAccesoRestringido() != null)
                return AccionAccesoRestringido();

            if (id == null) 
                return NotFound();

            var pedido = _repoPed.GetById(id);
            if (pedido == null)
                return NotFound();
            var pedidoView = _mapper.Map<ModificarPedidoViewModel>(pedido);
            pedidoView.IdPedido = pedido.Id;///NO se Mapea solo
            pedidoView.IdCliente = pedido.Cliente.Id;///NO se Mapea solo
            return View(pedidoView);
        }

        // POST: Cadete/Editar/5
        [HttpPost]
        public IActionResult Editar(ModificarPedidoViewModel pedidoView)
        {
            if (AccionAccesoRestringido() != null)
                return AccionAccesoRestringido();

            Pedido pedido = _mapper.Map<Pedido>(pedidoView);
            pedido.Id = pedidoView.IdPedido;///NO se Mapea solo
            _repoPed.Update(pedido);

            Cliente cliente = _mapper.Map<Cliente>(pedidoView);
            cliente.Id = pedidoView.IdCliente;///NO se Mapea solo
            _repoCli.Update(cliente);

            return RedirectToAction("Index");
        }

        // GET: Pedido/ConfirmacionBorrar/5
        public IActionResult ConfirmacionBorrar(int? id) 
        {
            if (AccionAccesoRestringido() != null)
                return AccionAccesoRestringido();

            if (id == null) 
                return NotFound();
            var pedido = _repoPed.GetById(id);
            if (pedido == null)
                return NotFound();
            var borrarView = _mapper.Map<BorrarPedidoViewModel>(pedido);
            return View(borrarView);
        }

        // [HttpGet]
        public IActionResult Borrar(int id) 
        {
            if (AccionAccesoRestringido() != null)
                return AccionAccesoRestringido();

            // if (id != null) 
            //     return NotFound();
            _repoPed.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Pedido/AsignarCadete/5
        public IActionResult AsignarCadete(int id)
        {
            if (AccionSinAcceso() != null)
                return AccionSinAcceso();

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
            if (AccionSinAcceso() != null)
                return AccionSinAcceso();
                
            int idCadete = asignarView.IdCadete;
            int idPedido = asignarView.IdPedido;
            _repoPed.AsignarCadeteAPedido(idCadete,idPedido);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}