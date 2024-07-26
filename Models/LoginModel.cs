namespace COMBUSTIBLEAESCORE.Models
{
    public class LoginModel
    {
        public int UsuarioID { get; set; }

        public string Username { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Correo { get; set; }

        public int Estado { get; set; }
        public int PerfilID { get; set; }
        //Data Compañia
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string Direccion { get; set; }

        /*public int MunicipioID { get; set; }

        public int DepartamentoID { get; set; }

        public string SitioWeb { get; set; }*/

        public bool Tutorial { get; set; }
        public string FechaActivacion { get; set; }
        public string TelMovil { get; set; }

       /* public string Telfijo { get; set; }

        public string RazonSocial { get; set; }

        public string Descripcion { get; set; }*/
    }
}
