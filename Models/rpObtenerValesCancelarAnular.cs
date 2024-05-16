namespace COMBUSTIBLEAESCORE.Models
{
    public class rpObtenerValesCancelarAnular
    {
        public int ValeCombustubibleID { get; set; }
        public string NumVale { get; set; }
        public string Placa { get; set; }
        public string FechaGeneracion { get; set; }
        public string UsuarioGenera { get; set; }
        public string Estado {  get; set; }
        public string TipoCarga { get; set; }
        //ublic string FechaRecibida { get; set;}
        public string CentroCosto { get;set; }
        public string CantidadGalones { get; set; }
        public double TotalPrecio { get; set; }
        public string Proyecto { get; set; }
        //public  int Aprobado { get; set; }  
        public string UsuarioAutoriza { get; set; }
        public string FechaAutorizado { get; set; }
        //public double Odometro { get; set; }

        public int Anulado { get; set; }

    }
}
