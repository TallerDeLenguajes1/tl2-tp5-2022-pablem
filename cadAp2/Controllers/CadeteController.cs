using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using ViewModels;
using Repositorios;


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

        // GET: Cadete/AsignarPedido/5
        public IActionResult AsignarPedido(int id)
        {
            ViewData["idCad"] = id;
            var listaPendientes = PedidoController.listaPedidos.Where(ped => ped.Estado == EstadoPedido.Pendiente);
            List<AsignarPedidoViewModel> asignarView = _mapper.Map<List<AsignarPedidoViewModel>>(listaPendientes);
            return View(asignarView);
        }

        // GET: Cadete/GuardarPedido?idCad=1&idPed=5
        public IActionResult GuardarPedido(int idCad, int idPed)
        {
            var cadete = listaCadetes.Single(x => x.Id == idCad);
            var pedido = PedidoController.listaPedidos.Single(x => x.Id == idPed);
            foreach (var cad in listaCadetes)
            {
                if (cad.ListaPedidos != null && cad.ListaPedidos.Contains(pedido))
                {
                    cad.ListaPedidos.Remove(pedido);
                    break;
                }
            }
            pedido.Estado = EstadoPedido.Viajando;
            cadete.ListaPedidos.Add(pedido);
            return RedirectToAction("Index",listaCadetes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}