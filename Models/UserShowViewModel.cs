using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicSite.Models.Domain;

namespace MusicSite.Models
{
    public class UserShowViewModel
    {
        public User User{ get; set; }
        public ActivityShare ActivityShare { get; set; }
        public List<Song> RecentSongs { get; set; }
        public List<Song> UploadedSongs { get; set; }
    }
}