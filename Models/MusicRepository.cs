using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicSite.Models.Domain;
using System.Diagnostics;
using System.Data;

namespace MusicSite.Models
{
    public class MusicRepository : IRepository
    {
        private DatabaseContext context = new DatabaseContext();

        public IEnumerable<Song> AllSongs
        {
            get { return context.Songs; }
        }

        public IEnumerable<Tag> AllTags
        {
            get { return context.Tags; }
        }

        public IEnumerable<User> AllUsers
        {
            get { return context.Users; }
        }

        public User FindUserByName(string name)
        {
            return context.Users.FirstOrDefault(user => user.Name == name);
        }

        public User FindUserById(int id)
        {
            return context.Users.FirstOrDefault(user => user.UserId == id);
        }

        public Song FindSongById(int id)
        {
            return context.Songs.FirstOrDefault(song => song.SongId == id);
        }

        public void AddSong(Song song)
        {
            song.Tags = song.Tags.Select(tag => GetOrCreateTag(tag.Name)).ToList();
            context.Songs.Add(song);
            SaveChanges();
        }

        public void AddUser(User user)
        {
            context.Users.Add(user);
            SaveChanges();
        }

        public void UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void UpdateSong(Song song)
        {
            Song existingSong = context.Songs.First(s => s.SongId == song.SongId);
            existingSong.Artist = song.Artist;
            existingSong.Title = song.Title;
            existingSong.Description = song.Description;
            existingSong.Tags.Clear();
            context.Entry(existingSong).State = EntityState.Modified;
            context.SaveChanges();
            existingSong.Tags = song.Tags.Select(tag => GetOrCreateTag(tag.Name)).ToList();
            context.Entry(existingSong).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void RemoveUser(int userId)
        {
            User user = context.Users.Find(userId);
            user.ListenInfos.Clear();
            user.UploadInfos.Clear();
            user.DownloadInfos.Clear();
            context.Users.Remove(user);
            SaveChanges();
        }

        public void RemoveSong(int songId)
        {
            Song song = context.Songs.Find(songId);
            context.Songs.Remove(song);
            SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Tag GetOrCreateTag(string name)
        {
            name = name.Trim().ToLower();
            Tag dbTag = context.Tags.FirstOrDefault(tag => tag.Name == name);
            return dbTag ?? new Tag { Name = name };
        }
    }
}