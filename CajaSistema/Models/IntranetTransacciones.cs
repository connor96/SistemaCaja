namespace CajaSistema.Models
{
    public class IntranetTransacciones
    {
        public int IdTransacciones { get; set; }
        public string? Idpersona { get; set; }
        public string? Periodo { get; set; }
        public bool? Estado { get; set; }
        public decimal? MontoTotal { get; set; }
        public int? IdSede { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string? UsuarioRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string? UsuarioActualizacion { get; set; }
    }
}
