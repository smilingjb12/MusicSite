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
            var rep = new MusicRepository();

            var user = new User { Name = "jack", Password = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220", ActivationCode = "1ab2", Email = "s@g.com", IsActivated = true };
            rep.AddUser(user);

            var userBill = new User { Name = "jim", Password = "1CD02E31B43620D7C664E038CA42A060D61727B9", ActivationCode = "135dfg", Email = "d@d.d", IsActivated = true };
            userBill.Role = "Admin";

            rep.AddUser(userBill);
        }
    }
}