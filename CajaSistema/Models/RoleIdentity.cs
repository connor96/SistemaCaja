using Microsoft.AspNetCore.Identity;

namespace CajaSistema.Models
{
    public class RoleIdentity:IdentityRole
    {
        public string observacion { get; set; }
    }
}
