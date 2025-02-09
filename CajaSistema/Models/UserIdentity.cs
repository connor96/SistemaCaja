using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CajaSistema.Models
{
    public class UserIdentity : IdentityUser
    {
        [Required(ErrorMessage = "Apellido Requerido")]
        [Display(Name = "Apellido Paterno")]
        public string? aPaterno { get; set; }

        
        [Display(Name = "Apellido Materno")]
        public string? aMaterno { get; set; }

        [Required(ErrorMessage = "Primer Nombre Requerido")]
        [Display(Name = "Primer Nombre")]
        public string? nombres1 { get; set; }

        [Display(Name = "Segundo Nombre")]
        public string? nombres2 { get; set; }

        [Display(Name = "Celular")]
        public string? celular { get; set; }

        [Required(ErrorMessage = "id Persona Necesario")]
        [Display(Name = "IdPersona")]
        public string? idPersona { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? password { get; set; }

        [Required (ErrorMessage ="Escriba el password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? confirmPassword { get; set; }

        [Required(ErrorMessage = "Seleccione un Rol")]
        [Display(Name = "Rol")]
        public string? rol { get; set; }
    }
}
