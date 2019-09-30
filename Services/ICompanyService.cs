using CompanyTest.Data;
using System.Collections.Generic;

namespace CompanyTest.Services
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetList();

        Company Add(string name);

        bool IsExist(string id);

        void Delete(string id);
    }
}
