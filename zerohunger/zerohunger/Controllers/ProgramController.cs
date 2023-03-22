using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zerohunger.EF;
using zerohunger.Auth;

namespace zerohunger.Controllers

{
    [Logged]
    public class ProgramController : Controller
    {
       
        // GET: Program
        [HttpGet]
        public ActionResult Dashboard()
        {
            var db = new zerohungerEntities1();
            return View(db.Restaurents.ToList());
            
        }

        [HttpGet]
        public ActionResult CollectionReq()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CollectionReq(Restaurent model) {
            var db = new zerohungerEntities1();
            db.Restaurents.Add(model);
            
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        public ActionResult Delete(int? id)
        {
            var db = new zerohungerEntities1();
            var pd = (from p in db.Restaurents
                      where p.id == id
                      select p).SingleOrDefault();
            return View(pd);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var db = new zerohungerEntities1();
            var pd = (from p in db.Restaurents
                      where p.id == id
                      select p).SingleOrDefault();

            if (pd != null)
            {
                db.Restaurents.Remove(pd);
                db.SaveChanges();
            }

            return RedirectToAction("Dashboard");
        }
        public ActionResult Edit(int id)
        {
            var db = new zerohungerEntities1();
            var pd = (from p in db.Restaurents
                      where p.id == id
                      select p).SingleOrDefault();
            return View(pd);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Restaurent model)
        {
            var db = new zerohungerEntities1();
            var pd = (from p in db.Restaurents
                      where p.id == model.id
                      select p).SingleOrDefault();
            pd.id = model.id;
            pd.name = model.name;
            pd.food = model.food;
            pd.quantity = model.quantity;
            pd.expiredate = model.expiredate;
            db.SaveChanges();
            

            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        public ActionResult CollectionDash()
        {
            var db = new zerohungerEntities1();
            return View(db.Restaurents.ToList());
            
        }

        public ActionResult Accept(int id)
        {
            zerohungerEntities1 db = new zerohungerEntities1();
            List<Collection> cart = null;
            if (Session["cart"] == null)
            {
                cart = new List<Collection>();
            }
            else
            {
                cart = (List<Collection>)Session["cart"];
            }

            var restaurent = db.Restaurents.Find(id);


            cart.Add(new Collection()
            {
                
                res_id = restaurent.id,
                res_name = restaurent.name,
                food_name = restaurent.food,
                quantity = 1,
                expire = restaurent.expiredate,
            });
            Session["cart"] = cart;

            TempData["Msg"] = "Successfully Added";
            return RedirectToAction("CollectionDash");
        }

       
        public ActionResult Reject(int id)
        {
            //var cart = (List<Collection>)Session["cart"];
            zerohungerEntities1 db = new zerohungerEntities1();
            var item = db.Restaurents.Find(id);
            if (item != null)
            {
                // Find the item in the list using LINQ
                //var item = cart.FirstOrDefault(c => c.res_id == id);
                if (item != null)
                {
                   // var db = new zerohungerEntities1();
                    var dbItem = new Collection();
                    
                        dbItem.res_id = item.id;
                        dbItem.res_name = item.name;
                        dbItem.food_name = item.food;
                        dbItem.quantity = item.quantity;
                        dbItem.expire = item.expiredate;
                        dbItem.emp_name = "Not Assigned";
                        dbItem.emp_id = null;
                        dbItem.status = "Rejected";
                        dbItem.ordertime = DateTime.Now.ToString();
                        db.Collections.Add(dbItem);
                        db.SaveChanges();
                        TempData["Msg"] = "Item updated successfully";
                        return RedirectToAction("Confirmation");
                }
            }
            TempData["Msg"] = "Item not found in cart";
            return RedirectToAction("CollectionDash");
        }



        public ActionResult List()
        {

            var cart = (List<Collection>)Session["cart"];
            if (cart != null)
            {
                return View(cart);
            }
            TempData["Msg"] = "Cart Empty";
            return RedirectToAction("CollectionDash");


        }

        [HttpGet]
        public ActionResult Assign(int? id)
        {
            var cart = (List<Collection>)Session["cart"];
            if (cart != null)
            {
                var item = cart.FirstOrDefault(c => c.res_id == id);
                if (item != null)
                {
                    return View(item);
                }
            }
            TempData["Msg"] = "Item not found in cart";
            return RedirectToAction("List");
        }

        [HttpPost]
        
        public ActionResult Assign(Collection model)
        {
            var cart = (List<Collection>)Session["cart"];
            if (cart != null)
            {
                // Find the item in the list using LINQ
                var item = cart.FirstOrDefault(c => c.res_id == model.res_id);
                if (item != null)
                {
                    // Update the item properties with the new values
                    item.res_id = model.res_id;
                    item.res_name = model.res_name;
                    item.food_name = model.food_name;
                    item.quantity = model.quantity;
                    item.expire = model.expire;
                    item.emp_id = model.emp_id;

                    // Store the updated list back into the session
                    Session["cart"] = cart;

                    // Update the item in the database
                    var db = new zerohungerEntities1();

                    var dbItem = new Collection();


                    if (dbItem != null)
                    {
                        var emp = db.Employees.FirstOrDefault(e => e.id == item.emp_id);
                        if (emp != null)
                        {
                            dbItem.res_id = item.res_id;
                            dbItem.res_name = item.res_name;
                            dbItem.food_name = item.food_name;
                            dbItem.quantity = item.quantity;
                            dbItem.expire = item.expire;
                            dbItem.emp_name = emp.name;
                            dbItem.emp_id = item.emp_id;
                            dbItem.status = "Delivered";
                            dbItem.ordertime = DateTime.Now.ToString();
                            db.Collections.Add(dbItem);
                            db.SaveChanges();
                        }
                    }
                   


                    TempData["Msg"] = "Item updated successfully";
                    return RedirectToAction("Confirmation");
                }
            }
            TempData["Msg"] = "Item not found in cart";
            return RedirectToAction("CollectionDash");
        }
        
       
        public ActionResult Confirmation()
        {
            var db = new zerohungerEntities1();
            return View(db.Collections.ToList());

        }
    }
}