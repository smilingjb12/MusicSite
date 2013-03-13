using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSite.Models.Domain
{
    public class DownloadInfo
    {
        public int DownloadInfoId { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
        public virtual Song Song { get; set; }
    }
}