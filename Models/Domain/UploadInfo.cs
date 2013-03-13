using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSite.Models.Domain
{
    public class UploadInfo
    {
        public int UploadInfoId { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
        public virtual Song Song { get; set; }
    }
}