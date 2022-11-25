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
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string? Password { get; set; }
        
    }
}