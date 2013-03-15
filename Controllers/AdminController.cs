using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicSite.Models;
using MusicSite.Models.Domain;

namespace MusicSite.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private IRepository repository;

        public AdminController(IRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Users()
        {
            IEnumerable<User> users = repository.AllUsers.Where(u => u.Role != "Admin");
            return View(users);
        }

        public ActionResult DeleteUser(int id)
        {
            User user = repository.FindUserById(id);
            repository.RemoveUser(user.UserId);
            TempData["notice"] = string.Format("User account with name {0} has been deleted", user.Name);
            return RedirectToAction("Users");
        }
    }
}
