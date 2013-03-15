using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicSite.Models.Domain;

namespace MusicSite.Models
{
    public interface IRepository
    {
        IEnumerable<Song> AllSongs { get; }
        IEnumerable<Tag> AllTags { get; }
        IEnumerable<User> AllUsers { get; }
        User FindUserByName(string name);
        User FindUserById(int id);
        Song FindSongById(int id);
        void AddSong(Song song);
        void AddUser(User user);
        void UpdateSong(Song song);
        void UpdateUser(User user);
        void RemoveSong(int id);
        void RemoveUser(int id);
        void SaveChanges();
        Tag GetOrCreateTag(string name);
    }
}