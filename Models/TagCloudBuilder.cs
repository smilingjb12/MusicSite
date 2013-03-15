using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicSite.Models;
using MusicSite.Models.Domain;

namespace MusicSite.Models
{
    public class TagCloudBuilder
    {
        private IRepository repository;

        public TagCloudBuilder(IRepository repository)
        {
            this.repository = repository;
        }

        public TagCloud GetTagCloud()
        {
            var tagCloud = new TagCloud();
            tagCloud.TagsCount = repository.AllTags.Count();
            List<MenuTag> menuTags = (from tag in repository.AllTags
                                      where tag.Songs.Count() > 0
                                      orderby tag.Name
                                      select new MenuTag
                                      {
                                          Tag = tag.Name,
                                          Count = tag.Songs.Count()
                                      }).ToList();
            foreach (MenuTag menuTag in menuTags)
            {
                menuTag.TagClass = "tag" + tagCloud.GetRankForTag(menuTag);
            }
            tagCloud.MenuTags = menuTags;
            return tagCloud;
        }

        private Tag GetOrCreateTag(string name)
        {
            name = name.Trim().ToLower();
            Tag dbTag = repository.AllTags.FirstOrDefault(tag => tag.Name == name);
            return dbTag ?? new Tag { Name = name };
        }
    }
}