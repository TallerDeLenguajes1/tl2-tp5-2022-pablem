using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using ViewModels;
using Repositorios;

namespace cadAp2.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PedidoController> _logger;

        public ClienteController(ILogger<PedidoController> logger, IMapper mapper)
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

        [HttpPost]
        public IActionResult Guardar(AltaPedidoViewModel pedidoViewModel) 
        {
            var cliente = _mapper.Map<Cliente>(pedidoViewModel);
            var repoCliente = new RepositorioClienteSQLite();
            repoCliente.Save(cliente);
            return RedirectToAction("Alta","Pedido");
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}