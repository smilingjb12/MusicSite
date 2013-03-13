using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TagLib;
using System.Diagnostics;

namespace MusicSite.Models
{
    public static class Mp3Parser
    {
        public static Id3Info GetMetaInfo(string filePath)
        {
            using (var tagFile = TagLib.File.Create(filePath))
            {
                var info = new Id3Info();
                Tag tag = tagFile.Tag;
                info.Artist = tag.Artists.Length > 0 ? tag.Artists[0] : "Unknown";
                info.Title = tag.Title ?? "Unknown";
                info.Album = tag.Album ?? "Unknown";

                Debug.WriteLine("GetMetaInfo()");
                Debug.WriteLine("info:");
                Debug.WriteLine(info);

                return info;
            }
        }
    }
}