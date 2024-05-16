using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface IrpVales
    {
        public Task<IEnumerable<rpValesCombustibleModel>> getRpValesCombustible(int userid, int companyid, string fini, string ffin);
    }
}
