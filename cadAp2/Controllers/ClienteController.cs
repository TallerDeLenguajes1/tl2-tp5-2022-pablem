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

        /*Métodos para la validación de acceso por niveles de permisos*/
        /*¿Inyeccion de dependencias? --> no me deja usar httpcontext estático, tiene que ser clase derivada de controller*/
        private IActionResult AccionSinAcceso()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("rol")))
                return RedirectToAction("Index", "Login");

            return null;
        }
        private IActionResult AccionAccesoRestringido() 
        {
            if (AccionSinAcceso() == null)
            {
                if (HttpContext.Session.GetString("rol") != RolUsuario.Administrador.ToString()) 
                {
                    TempData["mensaje"] = "No tiene acceso al menu para modificar, guardar o eliminar clientes";
                    return RedirectToAction("Index");
                }
            }
            return null;
        }
        /*Fin vlidacion de accesos*/

        public IActionResult Index()
        {
            if (AccionSinAcceso() != null)
                return AccionSinAcceso();

            var listaClientes = _repoCli.GetAll();
            var listaClientesView = _mapper.Map<List<MostrarClienteViewModel>>(listaClientes);
            return View(listaClientesView);
        }

        public IActionResult Alta()
        {
            if (AccionAccesoRestringido() != null)
                return AccionAccesoRestringido();

            ViewData["idC"] = _repoCli.ProxId();
            return View();
        }

        [HttpPost] //desde alta cliente 
        public IActionResult Guardar(AltaClienteViewModel clienteViewModel) 
        {
            if (AccionAccesoRestringido() != null)
                return AccionAccesoRestringido();

            var cliente = _mapper.Map<Cliente>(clienteViewModel);
            _repoCli.Save(cliente);
            return RedirectToAction("Alta","Pedido");
        }

        // GET: Cliente/Editar/5
        public IActionResult Editar(int? id)
        {
            if (AccionAccesoRestringido() != null)
                return AccionAccesoRestringido();

            if (id == null) 
                return NotFound();
            var cliente = _repoCli.GetById(id);
            if (cliente == null)
                return NotFound();
            var editarView = _mapper.Map<ModificarClienteViewModel>(cliente);
            return View(editarView);
        }

        [HttpPost]
        public IActionResult Editar(ModificarClienteViewModel clienteViewModel)
        {
            if (AccionAccesoRestringido() != null)
                return AccionAccesoRestringido();

            var cliente = _mapper.Map<Cliente>(clienteViewModel);
            _repoCli.Update(cliente);
            return RedirectToAction("Index");
        }

        public IActionResult ConfirmacionBorrar(int? id) 
        {
            if (AccionAccesoRestringido() != null)
                return AccionAccesoRestringido();

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
            if (AccionAccesoRestringido() != null)
                return AccionAccesoRestringido();
            
            // if (id != null) 
            //     return NotFound();
            _repoCli.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult PedidosCliente(int id)
        {
            if (AccionSinAcceso() != null)
                return AccionSinAcceso();

            var pedidosPorCliente = _repoPed.PedidosPorCliente(id);
            ViewData["titulo"] = "Pedidos de " + pedidosPorCliente.First().Cliente.Nombre;
            var pedidosView = _mapper.Map<List<MostrarPedidoViewModel>>(pedidosPorCliente);
            return View(pedidosView);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}