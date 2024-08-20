using COMBUSTIBLEAESCORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Interfaces
{
    public interface ILogin
    {
        public Task<IEnumerable<LoginModel>> login(string username, string password);

        public Task<IEnumerable<ModulosModel>> getModulos(string username, int CompanyID);

        //public Task<IEnumerable<string>> userExists(string username);
    }
}
