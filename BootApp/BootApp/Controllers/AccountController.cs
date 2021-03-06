﻿using System.Web.Mvc;
using System.Web.Security;
using BootApp.Models;

namespace BootApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }
 
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
 
        public ActionResult SignIn()
        {
            return View();
        }
 
        [HttpPost]
        public ActionResult SignIn(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.Username, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);
 
                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("","Ошибка при регистрации");
                }
            }
 
            return View(model);
        }

        public ActionResult Manage()
        {
            ListsOfStuff list = new ListsOfStuff();
            return View(list);
        }

        //[HttpPost]
       // public FileResult Manage(int unuseful)
       // {
         //   string username = this.Profile.UserName;
            //RedirectToAction("GetBibFile/username", "HomeController");
            //ListsOfStuff list = new ListsOfStuff();
            //return View(list);
           // return 
        //}
    }
    }


