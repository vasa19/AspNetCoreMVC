using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using vasa19.BookStore.Models;

namespace vasa19.BookStore.Controllers
{
    public class HomeController : Controller
    {
        [ViewData]
        public string CustomProperty { get; set; }
        [ViewData]
        public string Title { get; set; }
        public ViewResult Index()
        {
            Title = "Home Page";
            CustomProperty = "Custom Value";
            return View();
        }

        public ViewResult AboutUs()
        {
            Title = "About Page";
            return View();
        }

        public ViewResult ContactUs()
        {
            Title = "Contact Page";
            return View();
        }
    }
}
