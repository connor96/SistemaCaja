using Microsoft.AspNetCore.Identity;

namespace CajaSistema.Models
{
    public class UserIdentity : IdentityUser
    {
        public string aPaterno { get; set; }
        public string aMaterno { get; set; }
        public string nombres1 { get; set; }
        public string nombres2 { get; set; }
        public string celular { get; set; }
        public string idPersona { get; set; }
        public string dni { get; set; }

    }
}
