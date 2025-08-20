namespace CajaSistema.Models
{
    public class IntranetTransaccionesDetalle
    {
        public int IdTransaccionesDetalle { get; set; }
        public int? IdTransacciones { get; set; }
        public int? IdConceptoMatricula { get; set; }
        public int? IdConceptoOtros { get; set; }
        public int? IdDescuento { get; set; }
        public int? IdMatricula { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Monto { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string? UsuarioRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string? UsuarioActualizacion { get; set; }
        public string? idCiclo { get; set; }

    }
}
