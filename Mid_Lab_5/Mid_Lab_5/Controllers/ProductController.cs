using Mid_Lab_5.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Mid_Lab_5.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Product model)
        {
            var db = new Lab_5_MEntities();
            db.Products.Add(model);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var db = new Lab_5_MEntities();
            var products = db.Products.ToList();
            return View(products);
        }
        public ActionResult Edit(int id)
        {
            var db = new Lab_5_MEntities();
            var pd = (from p in db.Products
                      where p.id == id
                      select p).SingleOrDefault();
            return View(pd);

        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            var db = new Lab_5_MEntities();
            var expd = (from p in db.Products
                      where p.id == product.id
                      select p).SingleOrDefault();
            expd.id = product.id;
            expd.Cid = product.Cid;
            expd.Name = product.Name;
            expd.Price = product.Price;
            db.SaveChanges();
            return RedirectToAction("List");

        }

        public ActionResult Delete(int? id)
        {
            var db = new Lab_5_MEntities();
            var pd = (from p in db.Products
                      where p.id == id
                      select p).SingleOrDefault();
            return View(pd);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var db = new Lab_5_MEntities();
            var pd = (from p in db.Products
                      where p.id == id
                      select p).SingleOrDefault();

            if (pd != null)
            {
                db.Products.Remove(pd);
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }


    }
}