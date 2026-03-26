namespace CajaSistema.Models
{
    public class ProgramacionCursos
    {
        public string? CodCurso { get; set; }
        public string? CodNivelCurso { get; set; }
        public string? DesCurso { get; set; }
        public short Orden { get; set; }
        public byte Estado { get; set; }
    }
}
