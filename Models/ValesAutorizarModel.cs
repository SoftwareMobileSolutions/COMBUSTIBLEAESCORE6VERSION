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

    public class ValeAnteriorXMobile
    {
        public string NumVale { get; set; }
        /*public string FechaGeneracion { get; set; }
        public string FechaAutorizado { get;set; }*/
        public string FechaCierre { get; set; }
        public double CantidadGalones { get; set; }
        public double TotalPrecio { get; set; }
        public double EficienciaOdometro { get; set; }
        //public string EstatoVale { get;set; }

    }
}
