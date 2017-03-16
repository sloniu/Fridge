using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class DatabaseManager
    {
        private twitch10dblocalhostEntities db = new twitch10dblocalhostEntities();

        public void AddBuild(User user, Build build)
        {
            var a = db.Build.Count(b => b.PlayerName == build.PlayerName && b.MatchId == build.MatchId);

            if (a != 0) return;
            db.Build.Add(build);
            var u = new UserHasBuild()
            {
                UserId = user.UserId
            };
            db.UserHasBuild.Add(u);
            db.SaveChanges();
        }

        public void AddUser(string userName)
        {
            var user = new User()
            {
                UserName = userName
            };
            db.User.Add(user);
            db.SaveChanges();
        }
    }
}