using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSite.Models
{
    public class PagingInfo
    {
        public int Current { get; set; }
        public int Total { get; set; }
        public int PageSize { get; set; }
    }
}