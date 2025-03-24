namespace CajaSistema.Models
{
    public class ListaPeriodo
    {
        public int idPeriodoMatricula { get; set; }
        public string? periodoTexto { get; set; }
        public string? periodoDescripcion { get; set; }
        public bool estado { get; set; }
        public DateTime fechaInicioMatricula { get; set; }
        public DateTime fechaFinMatricula { get; set; }
    }
}
