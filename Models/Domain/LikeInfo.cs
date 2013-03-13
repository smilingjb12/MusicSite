﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSite.Models.Domain
{
    public class LikeInfo
    {
        public int LikeInfoId { get; set; }
        public virtual User User { get; set; }
        public virtual Song Song { get; set; }
    }
}