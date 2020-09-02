using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class AuthenticationController : Controller
    {

        QCEntities db = new QCEntities();

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            UserLogin UserLogin = new UserLogin();
            CreateVisitService createVisitService = new CreateVisitService();
           // createVisitService.CreateVisit();
            return View(UserLogin);

        }

        [HttpPost]
        public ActionResult Login(UserLogin MM, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Message = "";

            if (ModelState.IsValid)
            {
                var user = db.Users.Where(u => u.Username == MM.UserName && u.Password == MM.Password).Select(u => new { u.Id,u.Username, u.Password, u.JobTitle }).FirstOrDefault();
                //var user = new UserLogin { UserName = "admin", Password = "admin" };

               
                if (user != null)
                {
                    Session["UserId"] = user.Id.ToString();
                    Session["Username"] = user.Username.ToString();
                    Session["JobTitle"] = user.JobTitle.ToString();

                    //Session["CustomerID"] = user.CustomerId;
                    if (returnUrl == null)
                    {
                        //return View("~/Views/Home/Home.cshtml");
                        return RedirectToAction("MakeChoice", "Home");
                        //return Redirect(returnUrl);
                    }
                    else
                    {
                        //return Redirect(returnUrl);
                        return RedirectToAction("MakeChoice", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }


            }
            // If we got this far, something failed, redisplay form
            return View("Login", MM);
        }
    }
}