using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cadAp2.Models;
using ViewModels;
using Repositorios;


namespace cadAp2.Controllers
{
    public class CadeteController : Controller
    {
        static int numeroCadetes = 0;
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
            var listaCadete = cadeteRepo.GetAll();
            List<MostrarCadeteViewModel> listaView = _mapper.Map<List<MostrarCadeteViewModel>>(listaCadete);
            return View(listaView); 
        }

        public IActionResult AltaCadete()
        {
            ViewData["idCad"] = numeroCadetes+1;
            return View();
        }

        [HttpPost]
        public IActionResult GuardarCadete(AltaCadeteViewModel cadeteViewModel) 
        {
            //if(ModelState.IsValid)//---------------------> ? CONSULTA
            var cadeteRepo = new RepositorioCadeteSQLite();
            Cadete cadete = _mapper.Map<Cadete>(cadeteViewModel);
            ++numeroCadetes;
            cadeteRepo.Save(cadete);
            return RedirectToAction("Index",listaCadetes);
        }

        // GET: Cadete/Editar/5
        public IActionResult Editar(int? id)
        {
            if (id == null) 
                return NotFound();

            var cadete = listaCadetes.Single(x => x.Id == id);
            if (cadete == null)
                return NotFound();

            ModificarCadeteViewModel editarView = _mapper.Map<ModificarCadeteViewModel>(cadete);
            return View(editarView);
        }

        // POST: Cadete/Editar/5
        [HttpPost]
        public IActionResult Editar(ModificarCadeteViewModel cadeteViewModel)
        {
            Cadete actual = _mapper.Map<Cadete>(cadeteViewModel);
            Cadete anterior = listaCadetes.Single(x => x.Id == actual.Id);
            anterior.Nombre = actual.Nombre;
            anterior.Direccion = actual.Direccion;
            anterior.Telefono = actual.Telefono;//sorry not sorry --> TP6: UPDATE
            return RedirectToAction("Index",listaCadetes);
        }

        public IActionResult ConfirmacionBorrar(int id) 
        {
            ///////control if(listaCadetes.any)
            var cadete = listaCadetes.Single(x => x.Id == id); //.SingleOrDefault(); 
            BorrarCadeteViewModel borrarView = _mapper.Map<BorrarCadeteViewModel>(cadete);
            return View(borrarView);
        }

        // [HttpGet]
        public IActionResult BorrarCadete(int id) 
        {
            var cadete = listaCadetes.Single(x => x.Id == id);
            if(cadete.ListaPedidos != null && cadete.ListaPedidos.Any()) {
                foreach (var pedido in cadete.ListaPedidos) { 
                    if(pedido.Estado == EstadoPedido.Viajando)
                        pedido.Estado = EstadoPedido.Pendiente;   
                }
            }
            listaCadetes.Remove(cadete);
            return RedirectToAction("Index",listaCadetes);
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