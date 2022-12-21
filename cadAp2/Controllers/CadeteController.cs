using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using ViewModels;
using Repositorios;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace cadAp2.Controllers
{
    public class CadeteController : ControllerConValidacionAcceso
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CadeteController> _logger;
        private readonly IRepositorioCadete _repoCad;
        private readonly IRepositorioPedido _repoPed;
        private readonly IRepositorioCliente _repoCli;

        public CadeteController(ILogger<CadeteController> logger, 
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
            try
            {
                if (AccionSinAcceso() != null)
	                return AccionSinAcceso();
	
	            var cadetes = _repoCad.GetAll();
	            var cadetesView = _mapper.Map<List<MostrarCadeteViewModel>>(cadetes);
	            foreach (var cadView in cadetesView)
	            {
	                cadView.NroPendientes = _repoPed.PedidosPorCadete(cadView.Id).Select(x => x.Estado = EstadoPedido.Viajando).Count();
	            }
	            return View(cadetesView); 
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning(ex,"Index: Lista de cadetes");
                return View("/Views/Home/Error.cshtml");
            }
        }

        public IActionResult AltaCadete()
        {
            try
            {
                if (AccionAccesoRestringido() != null)
	                return AccionAccesoRestringido();
	
	            ViewData["idCad"] = _repoCad.GetLastId() + 1;
	            return View();
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning(ex,"Alta cadete formulario");
                return View("/Views/Home/Error.cshtml");
            }
        }

        [HttpPost]
        public IActionResult GuardarCadete(AltaCadeteViewModel cadeteViewModel) 
        {
            try
            {
                if (AccionAccesoRestringido() != null)
                    return AccionAccesoRestringido();
                
                var cadete = _mapper.Map<Cadete>(cadeteViewModel);
                _repoCad.Save(cadete);
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            { 
                _logger.LogWarning(ex,"No se guardó el cadete");   
                return View("/Views/Home/Error.cshtml");
            }
        }

        // GET: Cadete/Editar/5
        public IActionResult Editar(int? id)
        {
            try
            {
                if (AccionAccesoRestringido() != null)
	                return AccionAccesoRestringido();
	
	            if (id == null) 
                {
                    _logger.LogWarning("Editar cadete sin ID");   
                    return View("/Views/Home/Error.cshtml");
                }

	            var cadete = _repoCad.GetById(id);
	            if (cadete == null)
	            {
                    _logger.LogWarning($"Editar: no se encontró cadete con ID {id}");   
                    return View("/Views/Home/Error.cshtml");
                }

	            ModificarCadeteViewModel editarView = _mapper.Map<ModificarCadeteViewModel>(cadete);
	            return View(editarView);
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning(ex,"Editar cadete formulario");
                return View("/Views/Home/Error.cshtml");
            }
        }

        [HttpPost]
        public IActionResult Editar(ModificarCadeteViewModel cadeteViewModel)
        {
            try
            {
                if (AccionAccesoRestringido() != null)
	                return AccionAccesoRestringido();
	
	            var cadete = _mapper.Map<Cadete>(cadeteViewModel);
	            _repoCad.Update(cadete);
	            return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning(ex,"No se editó el cadete");
                return View("/Views/Home/Error.cshtml");
            }
        }

        public IActionResult ConfirmacionBorrar(int? id) 
        {
            try
            {
                if (AccionAccesoRestringido() != null)
	                return AccionAccesoRestringido();
	
	            if (id == null) 
	            {
	                _logger.LogWarning("Borrar cadete sin ID");   
	                return View("/Views/Home/Error.cshtml");
	            }
	            var cadete = _repoCad.GetById(id);
	            
	            if (cadete == null)
	            {
	                _logger.LogWarning($"Borrar: no se encontró cadete con ID {id}");   
	                return View("/Views/Home/Error.cshtml");
	            }
	
	            BorrarCadeteViewModel borrarView = _mapper.Map<BorrarCadeteViewModel>(cadete);
	            return View(borrarView);
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning(ex,"Confirmación para borrar cadete");
                return View("/Views/Home/Error.cshtml");
            }
        }

        // [HttpPost]
        public IActionResult BorrarCadete(int id)
        {
            try
            {
                if (AccionAccesoRestringido() != null)
                        return AccionAccesoRestringido();
                        
                    _repoCad.Delete(id);
                    return RedirectToAction("Index"); 
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning(ex,"No se borró el cadete");
                return View("/Views/Home/Error.cshtml");
            }
        }

        // GET: Cadete/AsignarPedido/5
        public IActionResult AsignarPedido(int id)
        {
           if (AccionSinAcceso() != null)
                return AccionSinAcceso();
            
            var asignarView = new AsignarPedidoViewModel();
            asignarView.IdCadete = id;
            /*creo una select list con pedidos*/
            // if(listaPendientes != null && listaPendientes.Any())
            var listaPendientes = _repoPed.GetAll()?.Where(ped => ped.Estado == EstadoPedido.Pendiente);
            /**/
            asignarView.Pedidos = new SelectList(listaPendientes, "Id", "Detalle");
            return View(asignarView);
        }

        [HttpPost]
        public IActionResult AsignarPedido(AsignarPedidoViewModel asignarView)
        {
            if (AccionSinAcceso() != null)
                return AccionSinAcceso();
            
            var idCadete = asignarView.IdCadete;
            var idPedido = asignarView.IdPedido;
            _repoPed.AsignarCadeteAPedido(idCadete,idPedido);
            return RedirectToAction("Index");
        }

        public IActionResult PedidosCadete(int id)
        {
            if (AccionSinAcceso() != null)
                return AccionSinAcceso();
            
            ViewData["titulo"] = "Pedidos de " + _repoCad.GetById(id)!.Nombre;
            var pedidosPorCadete = _repoPed.PedidosPorCadete(id);
            var pedidosView = _mapper.Map<List<MostrarPedidoViewModel>>(pedidosPorCadete);
            return View(pedidosView);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}