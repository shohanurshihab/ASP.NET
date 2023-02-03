using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mid_Lab_Task_2.Models;

namespace Mid_Lab_Task_2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Products()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }
        public ActionResult Profile() {

            Prods[] product = new Prods[10];
             
            for (int i = 0; i < 10; i++)
            {
                Prods p = new Prods();
                p.Id = i;
                p.Name = "P-" + i;
                p.Price = 507 + i*2;
                product[i] = p;
            } 
            return View(product);      
        }
    }
}