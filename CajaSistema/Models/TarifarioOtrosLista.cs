namespace CajaSistema.Models
{
    public class TarifarioOtrosLista
    {
        public int idTarifario { get; set; }
        public string? descripcion { get; set; }
        public int academico { get; set; }
        public int administrado { get; set; }
        public int tipoConcepto { get; set; }
        public decimal monto { get; set; }
        public bool estado { get; set; }
        public string? usuarioRegistro { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}
