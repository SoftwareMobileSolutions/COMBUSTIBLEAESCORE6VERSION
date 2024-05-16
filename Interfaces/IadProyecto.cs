namespace COMBUSTIBLEAESCORE.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using COMBUSTIBLEAESCORE.Models;
    public interface IadProyecto
    {
        public Task<IEnumerable<mensaje>> InsertarPEPS(string PEPS_NombreProyecto, string Responsable_Estado, int CompanyID, int UsuarioID);
        public Task<IEnumerable<PEPModel>> ObtenerPEPS(int CompanyID);
        public Task<IEnumerable<mensaje>> ActualizarProyecto (int CompanyID, int ProyectoID, string Responsable, int Estado, int Bandera);
        public Task<IEnumerable<mensaje>> EliminarProyecto(int ProyectoID, int CompanyID);
    } 
}
