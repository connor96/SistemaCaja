namespace CajaSistema.Models
{
    public class TablaConceptosLibres
    {
        public int idConceptosLibres { get; set; }
        public string? idPersona { get; set; }
        public string? nombres { get; set; }
        public string? descripcion { get; set; }
        public decimal monto { get; set; }
        public string? sede { get; set; }
        public int estado { get; set; }
    }
}
