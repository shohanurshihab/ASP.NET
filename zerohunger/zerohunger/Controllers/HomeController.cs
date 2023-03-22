using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using zerohunger.EF;
using zerohunger.Models;
using zerohunger.Auth;

namespace zerohunger.Controllers
{ 
    [Logged]
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            return View("Login");
        }
        [HttpPost]
        [AllowAnonymous]
        public  ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                zerohungerEntities1 db = new zerohungerEntities1();
                var user = (from u in db.Users
                            where u.username.Equals(login.Username)
                            && u.password.Equals(login.Password)
                            select u).SingleOrDefault();
                if (user != null)
                {
                    Session["user"] = user;
                    var returnUrl = Request["ReturnUrl"];
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Dashboard", "Program");
                }
                TempData["Msg"] = "Username Password Invalid";
                //Session["user"] = user;
            }
            return View(login);
    }
        public ActionResult Register()
        {
            return View();
        }
        
    }
}