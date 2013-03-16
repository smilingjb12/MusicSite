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
        public int PageSize = 2;

        public AdminController(IRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Users(int page = 1)
        {
            IEnumerable<User> users = repository.AllUsers.Where(u => u.Role != "Admin")
                .OrderBy(user => user.UserId);
            IEnumerable<User> usersForPage = users.Skip((page - 1) * PageSize).Take(PageSize);
            var model = new AdminUsersViewModel();
            model.Users = usersForPage;
            model.PagingInfo = new PagingInfo
            {
                Current = page,
                Total = 1 + users.Count() / PageSize,
                PageSize = PageSize
            };

            return View(model);
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
