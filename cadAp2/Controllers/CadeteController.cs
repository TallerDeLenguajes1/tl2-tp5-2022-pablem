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
        public static List<Cadete> listaCadetes = new List<Cadete>(); ////NO VA MAS (en el tp6)
        private readonly IMapper _mapper;
        private readonly ILogger<CadeteController> _logger;
        // private readonly IRepositorioCadete cadeteRepo;
        //private readonly IConfiguration config;

        // public CadeteController(ILogger<CadeteController> logger, IMapper mapper, IRepositorioCadete cadeteRepo)
        public CadeteController(ILogger<CadeteController> logger, IMapper mapper)//, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            // this.cadeteRepo = cadeteRepo;
            // this.config = config;
        }

        public IActionResult Index()
        {
            var cadeteRepo = new RepositorioCadeteSQLite();
            var listaCadeteView = cadeteRepo.GetAll();
            return View(listaCadeteView); 
        }

        public IActionResult AltaCadete()
        {
            var cadeteRepo = new RepositorioCadeteSQLite();
            ViewData["idCad"] = cadeteRepo.ProxId();
            return View();
        }

        [HttpPost]
        public IActionResult GuardarCadete(AltaCadeteViewModel cadeteViewModel) 
        {
            //if(ModelState.IsValid)//---------------------> ? CONSULTA
            var cadeteRepo = new RepositorioCadeteSQLite();
            var cadete = _mapper.Map<Cadete>(cadeteViewModel);
            cadeteRepo.Save(cadete);
            return RedirectToAction("Index");
        }

        // GET: Cadete/Editar/5
        public IActionResult Editar(int? id)
        {
            if (id == null) 
                return NotFound();//-------------------<<CONSULTA

            var cadeteRepo = new RepositorioCadeteSQLite();
            var cadete = cadeteRepo.GetCadete(id);

            if (cadete == null)
                return NotFound();

            ModificarCadeteViewModel editarView = _mapper.Map<ModificarCadeteViewModel>(cadete);
            return View(editarView);
        }

        [HttpPost]
        public IActionResult Editar(ModificarCadeteViewModel cadeteViewModel)
        {
            var cadeteRepo = new RepositorioCadeteSQLite();
            var cadete = _mapper.Map<Cadete>(cadeteViewModel);
            cadeteRepo.Update(cadete);
            return RedirectToAction("Index");
        }

        public IActionResult ConfirmacionBorrar(int? id) 
        {
            if (id == null) 
                return NotFound();

            var cadeteRepo = new RepositorioCadeteSQLite();
            var cadete = cadeteRepo.GetCadete(id);
            
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
            var cadeteRepo = new RepositorioCadeteSQLite();
            cadeteRepo.Delete(id);
            return RedirectToAction("Index"); ///Si borro cadete los pedidos viajando pasan a pendientes 
        }

        // public IActionResult PedidosCadete(int id) ///Para después
        // {
        //     var cadete = listaCadetes.Single(x => x.Id == id);
        //     return View(cadete);
        // }

        // GET: Cadete/AsignarCadete/5
        public IActionResult AsignarPedido(int id)
        {
            var asignarView = new AsignarPedidoViewModel();
            asignarView.IdCadete = id;
            /*creo una select list con pedidos*/
            var repoPedido = new RepositorioPedidoSQLite();
            // var listaPedidos = repoPedido.GetAll();
            // if(listaPedidos != null && listaPedidos.Any())
            var listaPendientes = repoPedido.GetAll()?.Where(ped => ped.Estado == EstadoPedido.Pendiente);
            /**/
            asignarView.Pedidos = new SelectList(listaPendientes, "Id", "DetalleCorto");
            return View(asignarView);
        }

        [HttpPost]
        public IActionResult AsignarPedido(AsignarPedidoViewModel asignarView)
        {
            var repoCadete = new RepositorioCadeteSQLite();
            repoCadete.AsignarPedido(asignarView);
            return RedirectToAction("Index");
        }

        public IActionResult PedidosCadete(int id)
        {
            var repoCadete = new RepositorioCadeteSQLite();
            ViewData["titulo"] = "Pedidos de " + repoCadete.GetCadete(id).Nombre; //esto es una prueba
            var repoPedido = new RepositorioPedidoSQLite();
            var listaPedidosView = repoPedido.PedidosPorCadete(id);
            return View(listaPedidosView);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}