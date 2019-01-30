using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrabalhoBlueOpex.Db;
using TrabalhoBlueOpex.Models;

namespace TrabalhoBlueOpex.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<Employee> _userManager;

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Name"] = (await _userManager.GetUserAsync(HttpContext.User)).Name;
            return View(await GetEmployeesByCharge());
        }

        public ReportController(AppDbContext appDbContext, UserManager<Employee> userManager)
        {
            _dbContext = appDbContext;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(employee);
                _dbContext.SaveChanges();

                return Created("GetReport", new { employee.Id });
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetReport(String charge)
        {
            return View("Index", await GetEmployeesByCharge(charge));
        }

        private async Task<IList<Employee>> GetEmployeesByCharge(String charge = null)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var teste = _userManager.Users.Select(u => u.Company.Name);

            var companyId = _userManager.Users
                .Where(u => u.Id == user.Id)
                .Select(e => e.Company.CompanyId)
                .First();

            var queryable = _dbContext.Employees.Where(e => e.Company.CompanyId == companyId);

            if (!String.IsNullOrEmpty(charge))
                queryable = queryable.Where(e => e.Charge.Equals(charge, StringComparison.CurrentCultureIgnoreCase));

            return queryable.ToList();
        }

    }
}
