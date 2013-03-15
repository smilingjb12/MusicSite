using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicSite.Models;
using MusicSite.Models.Domain;
using System.Diagnostics;

namespace MusicSite.Controllers
{
    public class HomeController : Controller
    {
        private Repository repository = new Repository();

        [Authorize(Roles="User")]
        public ViewResult Search(string query)
        {
            return View(viewName: "Search", model: query);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public JsonResult GetSongs()
        {
            if (Request.Form.AllKeys.Length != 0)
            {
                string query = Request.Form[0].ToLower();
                List<Song> songs = new List<Song>();
                IEnumerable<Song> matchedSongs = repository.AllSongs.Where(s => s.Artist.ToLower().Contains(query) || s.Title.ToLower().Contains(query));
                var serializedSongs = from song in matchedSongs
                                      select new { song.SongId, song.Artist, song.Title, song.ServerPath };
                return Json(new { songs = serializedSongs });
            }
            else
            {
                return Json(new { error = "no search string has been supplied" });
            }
        }

        public ViewResult Index()
        {
            var topSongs = repository.AllSongs
                .OrderByDescending(song => song.Likes)
                .Take(5);

            var tagCloud = repository.GetTagCloud();
            List<MenuTag> tags = tagCloud.MenuTags;
            ViewBag.TagCloudItems = tagCloud.MenuTags;
            return View(topSongs);
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
