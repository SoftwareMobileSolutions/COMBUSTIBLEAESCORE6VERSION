using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IadTutorial
    { 
        public Task<IEnumerable<mensaje>> ValidarPaso(int CompanyID, int Paso);
    }
}
