using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MusicSite.Models.Domain;

namespace MusicSite.Models
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            var rep = new MusicRepository();

            var user = new User { Name = "Jack", Password = "1234", ActivationCode = "1ab2", Email = "s@g.com", IsActivated = true };
            rep.AddUser(user);

            var userBill = new User { Name = "Bill", Password = "Bill", ActivationCode = "135dfg", Email = "d@d.d", IsActivated = true };
            userBill.Role = "Admin";

            rep.AddUser(userBill);
        }
    }
}