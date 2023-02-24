using Mid_Lab_Task_4.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mid_Lab_Task_4.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
      public ActionResult Products()
        {
            var db= new Lab_4Entities();
            var products = db.Products.ToList();
            return View(products);
        }
        [HttpGet]
    public ActionResult Prentry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Prentry(Product model)
        {
            var db =new Lab_4Entities();
            db.Products.Add(model);
            db.SaveChanges();
            return RedirectToAction("Products");             
        }
    }
}