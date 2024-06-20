namespace COMBUSTIBLEAESCORE.Models
{
    public class ValesGeneradosModel
    {
        public int ValeCombustubibleID { get; set; }
        public string NumVale {  get; set; }
        public string Placa { get; set; }
        public float TotalPrecio { get; set; }
        public float CantidadGalones { get; set; }
        public string FechaGeneracion { get; set; }
        public string EstadoVale { get; set; }
        public string UsuarioGenerador { get; set; }
        public string TipoCarga { get; set; }
        public int TipoCargaValeCombustibleID { get; set; }
    }
}
