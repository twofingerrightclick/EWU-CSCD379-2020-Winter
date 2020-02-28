﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Web.Controllers
{
    public class HomeController : Controller
    {
        public int sasdProperty { get; set; }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View("~/Views/Gifts/ListGifts.cshtml");
        }

        public IActionResult Html()
        {
            return View();
        }
    }
}
