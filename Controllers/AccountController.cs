using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MusicSite.Models;
using MusicSite.Models.Domain;
using System.Diagnostics;

namespace MusicSite.Controllers
{
    public class AccountController : Controller
    {
        private IRepository repository;

        public AccountController(IRepository repository)
        {
            this.repository = repository;
        }

        private ActionResult RenderViewIfNotAuthenticated()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToRoute(new { controller = "User", action = "Show" });
            }
            else
            {
                return View();
            }
        }

        public ActionResult LogOn()
        {
            return RenderViewIfNotAuthenticated();
        }

        public ActionResult SignUp()
        {
            return RenderViewIfNotAuthenticated();
        }

        public RedirectToRouteResult Activate(string code)
        {
            User user = repository.AllUsers.First(u => u.ActivationCode == code);
            user.IsActivated = true;
            repository.UpdateUser(user);
            TempData["notice"] = "Your account has been activated. You can login now";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult SignUp(User user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            User existingUser = repository.AllUsers
                .FirstOrDefault(u => u.Name == user.Name);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Nickname is already taken. Choose another");
                return View();
            }
            else
            {
                string activationCode = Guid.NewGuid().ToString();
                user.ActivationCode = activationCode;
                user.IsActivated = false;
                user.Role = "User";
                repository.AddUser(user);
                TempData["notice"] = string.Format("An email with activation link has been sent to {0}. Check it out", user.Email);
                EmailSender.SendActivationCode(user.Email, activationCode);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult LogOn(string name, string password, string returnUrl)
        {
            if (Membership.ValidateUser(name, password))
            {
                User user = repository.AllUsers.First(u => u.Name == name);
                if (!user.IsActivated)
                {
                    ModelState.AddModelError("", "Your account hasn't been activated yet. Check your email");
                    return View();
                }
                FormsAuthentication.SetAuthCookie(name, false);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Show", "User");
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect nickname or password. Try again");
                return View();
            }
        }

        public RedirectToRouteResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
