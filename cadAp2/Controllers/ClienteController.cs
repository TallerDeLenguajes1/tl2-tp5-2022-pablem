using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using ViewModels;
using Repositorios;

namespace cadAp2.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ClienteController> _logger;
        private readonly IRepositorioCadete _repoCad;
        private readonly IRepositorioPedido _repoPed;
        private readonly IRepositorioCliente _repoCli;

        public ClienteController(ILogger<ClienteController> logger, IMapper mapper, IRepositorioCadete repoCad, IRepositorioPedido repoPed, IRepositorioCliente repoCli)
        {
            _logger = logger;
            _mapper = mapper;
            _repoCad = repoCad;
            _repoPed = repoPed;
            _repoCli = repoCli;
        }

        public IActionResult Index()
        {
            var listaClientes = _repoCli.GetAll();
            var listaClientesView = _mapper.Map<List<MostrarClienteViewModel>>(listaClientes);
            return View(listaClientesView);
        }

        public IActionResult Alta()
        {
            ViewData["idC"] = _repoCli.ProxId();
            return View();
        }

        [HttpPost] //desde alta cliente 
        public IActionResult Guardar(AltaClienteViewModel clienteViewModel) 
        {
            var cliente = _mapper.Map<Cliente>(clienteViewModel);
            _repoCli.Save(cliente);
            return RedirectToAction("Alta","Pedido");
        }

        // GET: Cliente/Editar/5
        public IActionResult Editar(int? id)
        {
            if (id == null) 
                return NotFound();
            var cliente = _repoCli.GetById(id);
            if (cliente == null)
                return NotFound();
            ModificarClienteViewModel editarView = _mapper.Map<ModificarClienteViewModel>(cliente);
            return View(editarView);
        }

        [HttpPost]
        public IActionResult Editar(ModificarClienteViewModel clienteViewModel)
        {
            var cliente = _mapper.Map<Cliente>(clienteViewModel);
            _repoCli.Update(cliente);
            return RedirectToAction("Index");
        }

        public IActionResult ConfirmacionBorrar(int? id) 
        {
            if (id == null) 
                return NotFound();
            var cliente = _repoCli.GetById(id);
            if (cliente == null)
                return NotFound();
            BorrarClienteViewModel borrarView = _mapper.Map<BorrarClienteViewModel>(cliente);
            return View(borrarView);
        }

        // [HttpPost] ??
        public IActionResult Borrar(int id)
        {
            // if (id != null) 
            //     return NotFound();
            _repoCli.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult PedidosCliente(int id)
        {
            ViewData["titulo"] = "Pedidos de " + _repoCli.GetById(id).Nombre; //esto es una prueba
            var listaPedidosView = _repoPed.PedidosPorCliente(id);
            return View(listaPedidosView);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}