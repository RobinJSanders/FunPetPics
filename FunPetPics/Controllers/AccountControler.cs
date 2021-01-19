using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FunPetPics.Data;
using FunPetPics.Models;

namespace FunPetPics.Controllers
{
    
        [Route("Account")]
        public class AccountController : Controller
        {

        [Route("Index")]
        public IActionResult Index()
            {
                return View();
            }

            [Route("Login")]
            [HttpPost]
            public IActionResult Login(string email, string password)
            {
                UserModel dbUser;

                using (var c = new FunPetPicsContext(null))
                {
                    dbUser = c.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
                }


                if (dbUser != null && dbUser.Password == password)
                {
                    HttpContext.Session.SetString("username", dbUser.DisplayName);
                    return View("Success");
                }
                else
                {
                    ViewBag.error = "Invalid Account";
                    return View("Index");
                }
            }

            [Route("Logout")]
            [HttpGet]
            public IActionResult Logout()
            {
                HttpContext.Session.Remove("username");
                return RedirectToAction("Index");
            }

            public IActionResult CreateAccount()
            {
                return View();
            }

            [Route("ConfirmCreateAccount")]
            [HttpPost]
            public IActionResult ConfirmCreateAccount(string email, string displayName, string password, string repeatPassword)
            {

                var users = new List<UserModel>();
                using (var c = new FunPetPicsContext(null))
                {
                    users = c.Users.ToList();
                }


                string errormessage = "";

                if (password != repeatPassword)
                    errormessage += "Passwords do not match \n";
                if (users.FirstOrDefault(u => u.Email == email) != null)
                    errormessage += "An account with this email already exists \n";
                if (users.FirstOrDefault(u => u.DisplayName == displayName) != null)
                    errormessage += "This display name already exists \n";


                if (errormessage.Any())
                {
                    ViewBag.error = errormessage;
                    return View("CreateAccount");
                }

                UserModel dbUser = new UserModel
                {
                    Email = email,
                    DisplayName = displayName,
                    Password = password,
                };
                HttpContext.Session.SetString("username", dbUser.DisplayName);
                 return View("Success");
              
            }

        
    }
}
