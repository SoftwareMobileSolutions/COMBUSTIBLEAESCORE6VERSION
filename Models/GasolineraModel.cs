namespace COMBUSTIBLEAESCORE.Models
{
    public class GasolineraModel
    {
        public int EstacionServcioID { get; set; }
        public string Nombre { get; set; }
        public float Latitud {  get; set; }
        public float Longitud { get; set; }

        ///////////////////////////
        public string Codigo { get; set; }
        public string Ciudad {  get; set; }
        public string Gerente { get; set; }
        public string Telefono { get; set; }
        public string NIT { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
    }
}
