using Microsoft.AspNetCore.Mvc;
using CompanyTest.Services;
using CompanyTest.Data;
using System.Collections.Generic;
using System;

namespace CompanyTest.Controllers
{
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public IEnumerable<Company> List() => _companyService.GetList();

        [HttpPost]
        public Company Create(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Company name is null");

            return _companyService.Add(name);
        }

        [HttpDelete]
        public void Delete(string id)
        {
            _companyService.Delete(id);
        }
    }
}
