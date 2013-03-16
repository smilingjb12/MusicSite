using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MusicSite.Models.Domain
{
    public class User
    {
        public User()
        {
            ListenInfos = new List<ListenInfo>();
            DownloadInfos = new List<DownloadInfo>();
            UploadInfos = new List<UploadInfo>();
            LikeInfos = new List<LikeInfo>();
        }

        public int UserId { get; set; }

        [Display(Name="Nickname")]
        [Required(ErrorMessage="Nickname is required")]
        [RegularExpression("^[A-Za-z_][-_A-Za-z0-9]+$", 
            ErrorMessage="Nickname must begin with a letter and be at least 2 characters long")]
        public string Name { get; set; }

        [Required(ErrorMessage="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage="Email is required")]
        public string Email { get; set; }

        public bool IsActivated { get; set; }

        public string ActivationCode { get; set; }

        public string Role { get; set; }

        public virtual List<ListenInfo> ListenInfos { get; set; }
        public virtual List<DownloadInfo> DownloadInfos { get; set; }
        public virtual List<UploadInfo> UploadInfos { get; set; }
        public virtual List<LikeInfo> LikeInfos { get; set; }

        public List<Song> GetLikedSongs()
        {
            return LikeInfos.Select(info => info.Song).ToList();
        }

        public List<Song> GetListenedSongs()
        {
            return ListenInfos.Select(info => info.Song).ToList();
        }

        public List<Song> GetUploadedSongs()
        {
            return UploadInfos.OrderByDescending(info => info.Date)
                .Select(info => info.Song).ToList();
        }

        public ActivityShare GetActivityShare()
        {
            int uploads = UploadInfos.Count();
            int downloads = DownloadInfos.Count();
            int listenings = ListenInfos.Count();

            int totalActivities = uploads + downloads + listenings;

            double uploadShare = 100 * uploads / (double)totalActivities;
            double downloadShare = 100 * downloads / (double)totalActivities;
            double listeningShare = 100 * listenings / (double)totalActivities;

            var activityShare = new ActivityShare
            {
                UploadShare = uploadShare,
                DownloadShare = downloadShare,
                ListeningShare = listeningShare
            };
            return activityShare;
        }

        public List<ActivityInfo> GetActivities()
        {
            var activities = new List<ActivityInfo>();
            var listenActivities = ListenInfos.Select(info => new ActivityInfo { Event = "Listened", Date = info.Date, Song = info.Song });
            var uploadActivities = UploadInfos.Select(info => new ActivityInfo { Event = "Uploaded", Date = info.Date, Song = info.Song });
            var downloadActivities = DownloadInfos.Select(info => new ActivityInfo { Event = "Downloaded", Date = info.Date, Song = info.Song });
            return listenActivities.Concat(uploadActivities).Concat(downloadActivities).OrderByDescending(activity => activity.Date).ToList();
        }
    }
}