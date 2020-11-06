using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using Panel.Models;

namespace Panel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult AddJob(AddJobRequest addJobRequest)
        {
            return View();
        }
        public IActionResult Login(LoginInfo loginInfo)
        {
            HttpContext.Session.SetString("token", "123");
            return RedirectToAction("Index");
        }
        public IActionResult Logout(LoginInfo loginInfo)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        public IActionResult Signup(AddUserRequest loginInfo)
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
        private bool isAuth()
        {
            var token = HttpContext.Session.GetString("token");
            return !string.IsNullOrEmpty(token);
        }
    }
}
