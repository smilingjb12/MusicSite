using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicSite.Models.Domain;

namespace MusicSite.Models
{
    public class AdminUsersViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}