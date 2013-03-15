using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicSite.Models.Domain
{
    public class Song
    {
        public Song()
        {
            Tags = new List<Tag>();
        }
        
        [HiddenInput(DisplayValue=false)]
        public int SongId { get; set; }

        [Required(ErrorMessage="Artist name is required")]
        public string Artist { get; set; }

        [Required(ErrorMessage="Title is required")]
        public string Title { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public string FilePath { get; set; }

        public int Likes { get; set; }

        public virtual List<Tag> Tags { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Artist: {0}\n", Artist);
            sb.AppendFormat("Title: {0}\n", Title);
            sb.AppendFormat("Description: {0}\n", Description);
            sb.AppendFormat("FilePath: {0}\n", FilePath);
            sb.AppendFormat("Likes: {0}\n", Likes);
            sb.AppendFormat("Tags:\n");
            Tags.ToList().ForEach(tag => sb.AppendFormat("{0} ", tag.Name));
            return sb.ToString();
        }

        public string ServerPath
        {
            get { return FilePath != null ? FilePath.Substring(FilePath.IndexOf("\\Content")).Replace("\\", "/") : ""; }
        }
    }
}