using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class AsignarCadeteViewModel 
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        [Display(Name="Pedidos Pendientes")]
        public int NroPendientes { get; set; }

        // public void OnGet()
        // {
        //     ViewData["Numbers"] = Enumerable.Range(1,5)
        //         .Select(n => new SelectListItem {
        //     Value = n.ToString(),
        //     Text = n.ToString()
        // }).ToList();
        // }

    }
}