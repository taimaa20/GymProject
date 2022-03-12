using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication24.Controllers
{
    public class LoginController1 : Controller
    {

        private readonly GymContext _context;

        public LoginController1(GymContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
[HttpPost]


        public IActionResult Login(string username, string password)
        {
            
            var auth = _context.Login.Where(x => x.Email == username && x.Password == password).SingleOrDefault();

            if (auth != null)
            {


                switch (auth.TypeId)
                {
                    case 1:
                        {
                            HttpContext.Session.SetInt32("id", auth.EmploeeId.Value);
                            return RedirectToAction("Index", "Admin");
                        }

                    case 3:
                        {

                            HttpContext.Session.SetInt32("id", auth.EmploeeId.Value);
                            return RedirectToAction("Index", "Coach");
                        }
                    case 5:
                        {

                            HttpContext.Session.SetInt32("id", auth.EmploeeId.Value);
                            return RedirectToAction("Index", "Accountant");
                        }

                    case 7:
                        {

                            HttpContext.Session.SetInt32("id", auth.CustomerId.Value);
                            return RedirectToAction("Index", "Home");
                        }
                   
                }




            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "LoginController1");

        }

    }


}