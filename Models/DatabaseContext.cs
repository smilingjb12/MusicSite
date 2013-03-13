using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MusicSite.Models.Domain;

namespace MusicSite.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ListenInfo> ListenInfos { get; set; }
    }
}