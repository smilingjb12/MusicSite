using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MusicSite.Models.Domain;

namespace MusicSite.Models
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            using (var rep = new Repository())
            {
                var tagRock = new Tag { Name = "rock" };
                var tagPop = new Tag { Name = "pop" };
                var tagIdm = new Tag { Name = "idm" };
                var tagPostRock = new Tag { Name = "post-rock" };
                var tagAlternative = new Tag { Name = "alternative" };
                var tagDarkwave = new Tag { Name = "darkwave" };
                var tagIndie = new Tag { Name = "indie" };
                var tagExperimental = new Tag { Name = "experimental" };

                var songMer = new Song { Artist = "Chelsea Wolfe", Title = "Mer", Description = "Dark and wood", Likes = 122 };
                var songJars = new Song { Artist = "Chevelle", Title = "Jars", Description = "Alternative kind of catchy", Likes = 23 };
                var songMladic = new Song { Artist = "Godspeed You! Black Emperor", Title = "Mladic", Description = "I didn't study", Likes = 58 };
                var songGetGot = new Song { Artist = "Death Grips", Title = "Get Got", Description = "Paranoid", Likes = 178 };

                songGetGot.Description = @"![Picture](http://d3na4zxidw1hr4.cloudfront.net/site_media/uploads/images/post/d/death-grips/2e22m8y_png_630x900_q85.jpg)

###Death Grips

####Get Got

Official site: [here](http://thirdworlds.net/main/)
LastFM page: [here](http://www.last.fm/music/Death+Grips?ac=death%20grips)

Lyrics:

>Get get get get
Got got got got
Blood rush to my
Head lit hot lock
Poppin’ off the
Clockin’ wrist slit
Watch bent thought bot";

                songMer.Tags = new List<Tag> { tagDarkwave, tagIndie, tagExperimental };
                songJars.Tags = new List<Tag> { tagAlternative, tagRock };
                songMladic.Tags = new List<Tag> { tagPostRock, tagRock, tagAlternative, tagIndie };
                songGetGot.Tags = new List<Tag> { tagExperimental, tagIndie, tagAlternative };

                rep.AddSong(songMer);
                rep.AddSong(songJars);
                rep.AddSong(songMladic);
                rep.AddSong(songGetGot);

                var user = new User { Name = "Jack", Password = "1234", ActivationCode = "1ab2", Email = "s@g.com", IsActivated = true };
                user.ListenInfos = new List<ListenInfo> 
                { 
                    new ListenInfo { Song = songMer, User = user, Date = DateTime.Parse("11/1/2013") },
                    new ListenInfo { Song = songJars, User = user, Date = DateTime.Parse("11/2/2013") },
                    new ListenInfo { Song = songMladic, User = user, Date = DateTime.Parse("11/2/2013") }
                };
                user.DownloadInfos = new List<DownloadInfo> 
                { 
                    new DownloadInfo { Song = songJars, User = user, Date = DateTime.Parse("14/2/2013") },
                    new DownloadInfo { Song = songMladic, User = user, Date = DateTime.Parse("15/2/2013") }
                };
                user.UploadInfos = new List<UploadInfo>
                {
                    new UploadInfo { Song = songGetGot, User = user, Date = DateTime.Parse("3/2/2013") },
                    new UploadInfo { Song = songMer, User = user, Date = DateTime.Parse("2/12/2013") }
                };
                rep.AddUser(user);

                var userBill = new User { Name = "Bill", Password = "Bill", ActivationCode = "135dfg", Email = "d@d.d", IsActivated = true };
                userBill.Role = "Admin";
                rep.AddUser(userBill);
            }
        }
    }
}