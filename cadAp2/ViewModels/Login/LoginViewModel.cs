using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name="Nombre de Usuario")]
        [StringLength(50, MinimumLength = 3)] 
        public string? NikName { get; set; }

        [Required]
        [DataType(DataType.Password)] 
        public string? Password { get; set; }
        
    }
}