using MusicSite.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSite.Models
{
    public class ActivityInfo
    {
        public string Event { get; set; }
        public Song Song { get; set; }
        public DateTime Date { get; set; }
    }
}