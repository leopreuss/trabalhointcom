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
        protected UserManager<User> mUserManager;
        protected SignInManager<User> mSignInManager;
        protected AppDbContext appDbContext;

        public UserController(UserManager<User> mUserManager, SignInManager<User> mSignInManager, AppDbContext appDbContext)
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

        public async Task<IActionResult> SignIn()
        { 
            if (!appDbContext.Users.Any())
            {
                //Cria um usuario padrão para conseguir acessar o sistema, caso não exista
                var employee = appDbContext.Employee.Where(e => e.Cpf == "115.943.057-81").First();

                var admin = new User
                {
                    Email = "leopreuss@gmail.com",
                    Employee = employee,
                    Password = "123456"
                };

                admin.UserName = admin.Email;

                await mUserManager.CreateAsync(admin, admin.Password);
            }           
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoSignIn(string email, string password, string returnUrl)
        {
            var result = await mSignInManager.PasswordSignInAsync(email, password, true, false);

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
            return await SignIn();
        }
    }
}