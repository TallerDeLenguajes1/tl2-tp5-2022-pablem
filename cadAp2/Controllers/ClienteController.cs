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

        public ClienteController(ILogger<ClienteController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var repoCliente = new RepositorioClienteSQLite();
            var listaClientes = repoCliente.GetAll();
            var listaClientesView = _mapper.Map<List<MostrarClienteViewModel>>(listaClientes);
            return View(listaClientesView);
        }

        public IActionResult Alta()
        {
            var repoCliente = new RepositorioClienteSQLite();
            ViewData["idC"] = repoCliente.ProxId();
            return View();
        }

        [HttpPost] //desde alta cliente 
        public IActionResult Guardar(AltaClienteViewModel clienteViewModel) 
        {
            var repoCliente = new RepositorioClienteSQLite();
            var cliente = _mapper.Map<Cliente>(clienteViewModel);
            repoCliente.Save(cliente);
            return RedirectToAction("Alta","Pedido");
        }

        // GET: Cliente/Editar/5
        public IActionResult Editar(int? id)
        {
            if (id == null) 
                return NotFound();
            var repoCliente = new RepositorioClienteSQLite();
            var cliente = repoCliente.GetCliente(id);
            if (cliente == null)
                return NotFound();
            ModificarClienteViewModel editarView = _mapper.Map<ModificarClienteViewModel>(cliente);
            return View(editarView);
        }

        [HttpPost]
        public IActionResult Editar(ModificarClienteViewModel clienteViewModel)
        {
            var repoCliente = new RepositorioClienteSQLite();
            var cliente = _mapper.Map<Cliente>(clienteViewModel);
            repoCliente.Update(cliente);
            return RedirectToAction("Index");
        }

        public IActionResult ConfirmacionBorrar(int? id) 
        {
            if (id == null) 
                return NotFound();
            var repoCliente = new RepositorioClienteSQLite();
            var cliente = repoCliente.GetCliente(id);
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
            var repoCliente = new RepositorioClienteSQLite();
            repoCliente.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult PedidosCliente(int id)
        {
            var repoCliente = new RepositorioClienteSQLite();
            ViewData["titulo"] = "Pedidos de " + repoCliente.GetCliente(id).Nombre; //esto es una prueba
            var repoPedido = new RepositorioPedidoSQLite();
            var listaPedidosView = repoPedido.PedidosPorCliente(id);
            return View(listaPedidosView);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}