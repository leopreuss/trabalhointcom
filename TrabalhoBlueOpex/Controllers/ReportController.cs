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
using Microsoft.EntityFrameworkCore;

namespace TrabalhoBlueOpex.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private Employee _employee = null;

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<Employee> GetEmployeeFromUser()
        {
            if (_employee == null)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                _employee = _dbContext.Users
                    .Where(u => u.Id == user.Id)
                    .Select(u => u.Employee)
                    .Include(e => e.Company)
                    .First();
            }
                
            return _employee;

        }

        public async Task<IActionResult> Index()
        {
            return await GetReport();
        }

        public ReportController(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _dbContext = appDbContext;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Company = (await GetEmployeeFromUser()).Company;
                _dbContext.Add(employee);
                _dbContext.SaveChanges();

                return Created("GetReport", new { employee.Id });
            }

            return BadRequest(ModelState);
        }

        public IActionResult SendEmployee()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetReport(String charge = null)
        {
            ViewData["Name"] = (await GetEmployeeFromUser()).Name;
            return View("Index", await GetEmployeesByCharge(charge));
        }

        private async Task<IList<Employee>> GetEmployeesByCharge(String charge = null)
        {

            var companyId = (await GetEmployeeFromUser()).CompanyId;
            var queryable = _dbContext.Employee.Where(e => e.Company.CompanyId == companyId);

            if (!String.IsNullOrEmpty(charge))
                queryable = queryable.Where(e => e.Charge.Equals(charge, StringComparison.CurrentCultureIgnoreCase));

            return queryable.ToList();
        }

    }
}
