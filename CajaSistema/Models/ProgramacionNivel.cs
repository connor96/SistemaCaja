namespace CajaSistema.Models
{
    public class ProgramacionNivel
    {
        public string? CodNivelCurso { get; set; }
        public short IdMallaCurricular { get; set; }
        public string? DesNivelCurso { get; set; }
        public short Orden { get; set; }
        public byte Estado { get; set; }
        public bool? EsCurso { get; set; }

    }
}
