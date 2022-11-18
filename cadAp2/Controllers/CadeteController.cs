using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using ViewModels;
using Repositorios;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace cadAp2.Controllers
{
    public class CadeteController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CadeteController> _logger;
        private readonly IRepositorioCadete _repoCad;
        private readonly IRepositorioPedido _repoPed;
        private readonly IRepositorioCliente _repoCli;
        //private readonly IConfiguration config;

        public CadeteController(ILogger<CadeteController> logger, IMapper mapper, IRepositorioCadete repoCad, IRepositorioPedido repoPed, IRepositorioCliente repoCli)//, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _repoCad = repoCad;
            _repoPed = repoPed;
            _repoCli = repoCli;
            // this.config = config;
        }

        public IActionResult Index()
        {
            var listaCadeteView = _repoCad.GetAll();
            return View(listaCadeteView); 
        }

        public IActionResult AltaCadete()
        {
            ViewData["idCad"] = _repoCad.ProxId();
            return View();
        }

        [HttpPost]
        public IActionResult GuardarCadete(AltaCadeteViewModel cadeteViewModel) 
        {
            //if(ModelState.IsValid)//---------------------> ? CONSULTA
            var cadete = _mapper.Map<Cadete>(cadeteViewModel);
            _repoCad.Save(cadete);
            return RedirectToAction("Index");
        }

        // GET: Cadete/Editar/5
        public IActionResult Editar(int? id)
        {
            if (id == null) 
                return NotFound();//-------------------<<CONSULTA
            var cadete = _repoCad.GetById(id);
            if (cadete == null)
                return NotFound();
            ModificarCadeteViewModel editarView = _mapper.Map<ModificarCadeteViewModel>(cadete);
            return View(editarView);
        }

        [HttpPost]
        public IActionResult Editar(ModificarCadeteViewModel cadeteViewModel)
        {
            var cadete = _mapper.Map<Cadete>(cadeteViewModel);
            _repoCad.Update(cadete);
            return RedirectToAction("Index");
        }

        public IActionResult ConfirmacionBorrar(int? id) 
        {
            if (id == null) 
                return NotFound();
            var cadete = _repoCad.GetById(id);
            
            if (cadete == null)
                return NotFound();

            BorrarCadeteViewModel borrarView = _mapper.Map<BorrarCadeteViewModel>(cadete);
            return View(borrarView);
        }

        // [HttpPost] ??
        public IActionResult BorrarCadete(int id)
        {
            // if (id != null) 
            //     return NotFound();
            _repoCad.Delete(id);
            return RedirectToAction("Index"); ///Si borro cadete los pedidos viajando pasan a pendientes 
        }

        // GET: Cadete/AsignarCadete/5
        public IActionResult AsignarPedido(int id)
        {
            var asignarView = new AsignarPedidoViewModel();
            asignarView.IdCadete = id;
            /*creo una select list con pedidos*/
            // var listaPendientes = repoPedido.GetPendientes();
            // if(listaPendientes != null && listaPendientes.Any())
            var listaPendientes = _repoPed.GetAll()?.Where(ped => ped.Estado == EstadoPedido.Pendiente);
            /**/
            asignarView.Pedidos = new SelectList(listaPendientes, "Id", "DetalleCorto");
            return View(asignarView);
        }

        [HttpPost]
        public IActionResult AsignarPedido(AsignarPedidoViewModel asignarView)
        {
            _repoCad.AsignarPedido(asignarView);
            return RedirectToAction("Index");
        }

        public IActionResult PedidosCadete(int id)
        {
            ViewData["titulo"] = "Pedidos de " + _repoCad.GetById(id)!.Nombre;
            var listaPedidosView = _repoPed.PedidosPorCadete(id);
            return View(listaPedidosView);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}