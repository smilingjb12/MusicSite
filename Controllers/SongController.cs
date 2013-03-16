using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicSite.Models;
using MusicSite.Models.Domain;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;

namespace MusicSite.Controllers
{
    [Authorize(Roles="User")]
    public class SongController : Controller
    {
        private IRepository repository;

        public SongController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public JsonResult Like(int songId)
        {
            User liker = repository.FindUserByName(User.Identity.Name);
            Song song = repository.FindSongById(songId);
            List<Song> likedSongs = liker.GetLikedSongs();
            if (likedSongs.FirstOrDefault(s => s.SongId == songId) != null)
            {
                liker.LikeInfos.RemoveAll(info => info.Song.SongId == songId);
                song.Likes -= 1;
            }
            else
            {
                liker.LikeInfos.Add(new LikeInfo { Song = song, User = liker });
                song.Likes += 1;
            }
            repository.SaveChanges();
            return Json(new { likes = song.Likes });
        }

        public ActionResult Edit(int id)
        {
            Song song = repository.FindSongById(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            User user = repository.FindUserByName(User.Identity.Name);
            List<Song> userUploadedSongs = user.GetUploadedSongs();
            if (userUploadedSongs.FirstOrDefault(s => s.SongId == song.SongId) == null)
            {
                TempData["notice"] = "You can not edit this song. You didn't upload it";
                return RedirectToAction("Show", "User");
            }
            else
            {
                return View(song);
            }
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude="Tags")] Song song)
        {
            if (ModelState.IsValid)
            {
                song.Tags = ParseTagString(Request["Tags"]);
                if (song.Tags.Count > 0)
                {
                    repository.UpdateSong(song);
                    return RedirectToAction("Show", "User");
                }
                else
                {
                    ModelState.AddModelError("Tags", "At least one tag must be present");
                    return View(song);
                }
            }
            else
            {

                return View(song);
            }
        }

        private List<Tag> ParseTagString(string tagString)
        {
            string parsedTagString = Regex.Replace(tagString, @",\s+", ",");
            parsedTagString = Regex.Replace(tagString, @"\s+", " ");
            parsedTagString = Regex.Replace(parsedTagString, @"\s+,", ",");
            if (parsedTagString == "")
            {
                return new List<Tag>();
            }
            else
            {
                return parsedTagString.Split(',').Select(tagStr => new Tag { Name = tagStr }).ToList();
            }
        }

        public ActionResult Show(int id)
        {
            Song song = repository.FindSongById(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        public ActionResult Download(int id)
        {
            Song song = repository.FindSongById(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            User downloader = repository.FindUserByName(User.Identity.Name);
            var downloadInfo = new DownloadInfo { Date = DateTime.Now, User = downloader, Song = song };
            downloader.DownloadInfos.Add(downloadInfo);
            repository.UpdateUser(downloader);
            return File(song.FilePath, "audio/mpeg", Path.GetFileName(song.FilePath));
        }
    }
}
