namespace COMBUSTIBLEAESCORE.Models
{
    public class rpLineaTimpoValeModel
    {
        public string NumVale { get; set; }
        public string Placa { get; set; }
        public string FechaGenerado { get; set; }
        public string UsuarioGenera { get; set; }
        public string FechaAutorizado { get; set; }
        public string UsuarioAutoriza { get; set; }
        public string FechaCancelacion { get; set; }
        public string UsuarioCancela { get; set; }
        public string FechaAnulacion { get; set; }
        public string UsuarioAnula { get; set; }
        public string Estado { get; set; }
        public bool Anulado { get; set; }
    }
}
