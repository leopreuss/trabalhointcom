using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrabalhoBlueOpex.Db;
using TrabalhoBlueOpex.Models;

namespace TrabalhoBlueOpex.Controllers
{
    public class UserController : Controller
    {
        protected UserManager<Employee> mUserManager;
        protected SignInManager<Employee> mSignInManager;
        protected AppDbContext appDbContext;

        public UserController(UserManager<Employee> mUserManager, SignInManager<Employee> mSignInManager, AppDbContext appDbContext)
        {
            this.mUserManager = mUserManager;
            this.mSignInManager = mSignInManager;
            this.appDbContext = appDbContext;
        }

        [Authorize]
        public IActionResult Index()
        { 
            return RedirectToAction("Index", "Main");
        }

        public async Task<IActionResult> SeedDatabase()
        {
            var companyIntcom = new Company("Intcom");
            var companyGoogle = new Company("Google");
            var companyMicrosoft = new Company("Microsoft");

            var employee1 = new Employee(companyIntcom)
            {
                Name = "João",
                Email = "joao@intcom.com.br",
                Password = "123456",
                Cpf = "313.766.277-01",
                DateOfAdmission = new DateTime(2017, 3, 5),
                BirthDate = new DateTime(1990, 5, 10),
                Charge = "Desenvolvedor",
                Identifier = 1111,
                UserName = "joaointcom"
            };

            var employee2 = new Employee(companyIntcom)
            {
                Name = "Roberto",
                Email = "roberto@intcom.com.br",
                Password = "123456",
                Cpf = "270.834.872-86",
                DateOfAdmission = new DateTime(2014, 3, 2),
                BirthDate = new DateTime(1985, 5, 1),
                Charge = "Analista",
                Identifier = 2222,
            };

            var employee3 = new Employee(companyGoogle)
            {
                Name = "Sérgio",
                Email = "sergio@google.com.br",
                Password = "123456",
                Cpf = "524.340.868-96",
                DateOfAdmission = new DateTime(2012, 5, 8),
                BirthDate = new DateTime(1988, 3, 6),
                Charge = "Administrativo",
                Identifier = 213526,
            };
            var employee4 = new Employee(companyMicrosoft)
            {
                Name = "Juliana",
                Email = "juliana@hotmail.com.br",
                Password = "123456",
                Cpf = "919.723.232-70",
                DateOfAdmission = new DateTime(2017, 1, 23),
                BirthDate = new DateTime(1991, 2, 25),
                Charge = "Designer",
                Identifier = 5449858,
            };

            var employee5 = new Employee(companyIntcom)
            {
                Name = "Bruno",
                Email = "bruno@intcom.com.br",
                Password = "123456",
                Cpf = "487.977.374-37",
                DateOfAdmission = new DateTime(2011, 6, 12),
                BirthDate = new DateTime(1982, 10, 6),
                Charge = "Financeiro",
                Identifier = 3434,
            };

            employee1.UserName = employee1.Email;
            employee2.UserName = employee2.Email;
            employee3.UserName = employee3.Email;
            employee4.UserName = employee4.Email;
            employee5.UserName = employee5.Email;

            var a = mUserManager.CreateAsync(employee1, employee1.Password);
            var b = mUserManager.CreateAsync(employee2, employee2.Password);
            var c = mUserManager.CreateAsync(employee3, employee3.Password);
            var d = mUserManager.CreateAsync(employee4, employee4.Password);
            var e = mUserManager.CreateAsync(employee5, employee5.Password);

            if ((await a).Succeeded
                && (await b).Succeeded
                && (await c).Succeeded
                && (await d).Succeeded
                && (await e).Succeeded)
                return Ok();

            return BadRequest();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoSignIn(string email, string password, string returnUrl)
        {
            var result = await mSignInManager.PasswordSignInAsync(email, password, true, false);

            var e = appDbContext.Employees.FirstOrDefault();

            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction("Index", "Report");
                return RedirectToAction(returnUrl);
            }

            ViewData["success"] = false;
            ViewData["message"] = "Nome do usuário ou senha inválidos";

            return View("SignIn");
        }

        public async Task<IActionResult> SignOut()
        {
            await mSignInManager.SignOutAsync();
            return SignIn();
        }
    }
}