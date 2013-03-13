using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSite.Models
{
    public class ActivityShare
    {
        public double DownloadShare { get; set; }
        public double UploadShare { get; set; }
        public double ListeningShare { get; set; }

        public bool HasAny
        {
            get { return DownloadShare + UploadShare + ListeningShare > 0; }
        }
    }
}