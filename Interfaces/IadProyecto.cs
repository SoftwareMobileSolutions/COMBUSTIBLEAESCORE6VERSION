namespace COMBUSTIBLEAESCORE.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using COMBUSTIBLEAESCORE.Models;
    public interface IadProyecto
    {
        public Task<IEnumerable<mensaje>> InsertarProyectos(string CodigoProyecto_NombreProyecto, string Responsable_Estado, int CompanyID, int UsuarioID);
        public Task<IEnumerable<ProyectoModel>> ObtenerProyectos(int CompanyID);
        public Task<IEnumerable<mensaje>> ActualizarProyecto (int CompanyID, int ProyectoID, string Responsable, int Estado, int Bandera);
        public Task<IEnumerable<mensaje>> EliminarProyecto(int ProyectoID, int CompanyID);
    } 
}
