using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicSite.Models;
using MusicSite.Models.Domain;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace MusicSite.Controllers
{
    [Authorize(Roles="User")]
    public class UserController : Controller
    {
        private Repository repository = new Repository();

        public ViewResult Show()
        {
            User user = repository.FindUserByName(User.Identity.Name);

            var model = new UserShowViewModel();
            model.User = user;
            model.ActivityShare = user.GetActivityShare();

            List<Song> recentSongs = user.ListenInfos
                .OrderByDescending(info => info.Date)
                .Select(info => info.Song)
                .Take(5)
                .ToList();

            model.Activities = user.GetActivities();
            model.UploadedSongs = user.GetUploadedSongs();

            return View(model);
        }

        public ViewResult Upload()
        { 
            return View();
        }

        public ViewResult UploadedSongs()
        {
            User user = repository.FindUserByName(User.Identity.Name);
            return View(user.GetUploadedSongs());
        }

        public ViewResult Activities()
        {
            User user = repository.FindUserByName(User.Identity.Name);
            return View(user.GetActivities());
        }

        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase file)
        {
            byte[] fileBytes = FileUtils.ReadBytesFromStream(HttpContext.Request.InputStream);
            string fileName = string.Format("{0}.mp3", Guid.NewGuid());
            string path = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
            System.IO.File.WriteAllBytes(path, fileBytes);
            Id3Info info = Mp3Parser.GetMetaInfo(path);
            Song uploadedSong = new Song
            {
                Artist = info.Artist,
                Title = info.Title,
                Description = string.Format("{0} by {1} from album \"{2}\"", info.Title, info.Artist, info.Album),
                FilePath = path,
                Tags = new List<Tag> { new Tag { Name = info.Artist } }
            };
            repository.AddSong(uploadedSong);
            User uploader = repository.FindUserByName(HttpContext.User.Identity.Name);
            uploader.UploadInfos.Add(new UploadInfo { Date = DateTime.Now, Song = uploadedSong, User = uploader });
            repository.UpdateUser(uploader);

            return Json(new { id = uploadedSong.SongId });
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
