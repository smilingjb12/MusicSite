using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace MusicSite.Models
{
    public class Id3Info
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Artist: {0}\n", Artist);
            sb.AppendFormat("Title: {0}\n", Title);
            sb.AppendFormat("Album: {0}\n", Album);
            return sb.ToString();
        }
    }
}