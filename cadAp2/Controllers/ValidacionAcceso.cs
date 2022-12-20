using Microsoft.AspNetCore.Mvc;
using Models;

namespace cadAp2.Controllers
{
    public class ControllerConValidacionAcceso : Controller
    {
        protected IActionResult AccionSinAcceso()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("rol")))
                return RedirectToAction("Index", "Login");

            return null;
        }

        protected IActionResult AccionAccesoRestringido() 
        {
            if (AccionSinAcceso() == null)
            {
                if (HttpContext.Session.GetString("rol") != RolUsuario.Administrador.ToString()) 
                {
                    TempData["mensaje"] = "No tiene acceso al menu para modificar, guardar o eliminar cadetes";
                    return RedirectToAction("Index");
                }
            }
            return null;
        }
    }
}