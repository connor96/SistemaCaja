namespace CajaSistema.Models
{
    public class ConceptosLibres
    {
        public int idConceptosLibres { get; set; }
        public string? idPersona { get; set; }
        public string? descripcion { get; set; }
        public string? idSede { get; set; }
        public string? periodo { get; set; }
        public int estado { get; set; }
        public int idTransaccion { get; set; }
        public decimal monto { get; set; }
        public string? usuarioRegistro { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}
