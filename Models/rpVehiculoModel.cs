namespace COMBUSTIBLEAESCORE.Models
{
    public class rpVehiculoModel
    {
        public int MobileID { get; set; }
        public string Placa {  get; set; }
        public string Nombre { get; set; }
        public string NombreSubfleet { get; set; }
        public int SubfleetID { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string FechaCreacion { get; set; }
        public float KmXGalon { get; set; }
        public float CapacidadTanque { get; set; }
        public string TipoCombustible { get; set; }
        public int TipoCombustibleID { get; set; }
        public string VIN {  get; set; }

        /*public string Placa { get; set; }
        public string Nombre { get; set; }
        public string NombreSubfleet { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string FechaCreacion { get; set; }
        public float KmXGalon { get; set; }
        public float CapacidadTanque { get; set; }
        public string TipoCombustible { get; set; }
        public string VIN { get; set; }*/

    }
}
