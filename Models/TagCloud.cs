using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSite.Models
{
    public class TagCloud
    {
        public int TagsCount { get; set; }
        public List<MenuTag> MenuTags { get; set; }

        public int GetRankForTag(MenuTag tag)
        {
            if (TagsCount == 0)
            {
                return 1;
            }

            double result = (tag.Count * 100) / TagsCount;
            if (result <= 1) return 1;
            if (result <= 4) return 2;
            if (result <= 8) return 3;
            if (result <= 12) return 4;
            if (result <= 18) return 5;
            if (result <= 30) return 6;
            return result <= 50 ? 7 : 8;
        }
    }
}