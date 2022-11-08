using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class AsignarPedidoViewModel 
    {
        public int Id { get; set; }
         [Display(Name="Detalle")]
        public string? DetalleCorto { get; set; }
        [Display(Name="Dirección")]
        public string? Direccion { get; set; }
    }
}