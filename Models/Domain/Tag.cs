using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSite.Models.Domain
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }

        public virtual List<Song> Songs { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}