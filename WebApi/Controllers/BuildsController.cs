using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class BuildsController : ApiController
    {
        private twitch10dblocalhostEntities db = new twitch10dblocalhostEntities();

        // GET: api/Builds
        public IQueryable<Build> GetBuild()
        {
            return db.Build;
        }

        [Route("api/Builds/{region}/{name}")]
        public Build Get(string region, string name)
        {
            var parser = new JsonParser();
            return parser.GetLastBuild(region, name);
        }

        // GET: api/Builds/5
        [ResponseType(typeof(Build))]
        public IHttpActionResult GetBuild(int id)
        {
            Build build = db.Build.Find(id);
            if (build == null)
            {
                return NotFound();
            }

            return Ok(build);
        }

        // PUT: api/Builds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBuild(int id, Build build)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != build.BuildId)
            {
                return BadRequest();
            }

            db.Entry(build).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Builds
        [Route("api/Builds/{token}/{region}/{playerName}/{matchId}")]
        public IHttpActionResult PostBuild(string token, string region, string playerName, string matchId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbm = new DatabaseManager();
            AuthenticateToken.Instance.Authenticate(token);
            var user = db.User.FirstOrDefault(u => u.UserName == AuthenticateToken.Instance.UserName);
            if (user == null)
            {
                dbm.AddUser(AuthenticateToken.Instance.UserName);
                user = db.User.FirstOrDefault(u => u.UserName == AuthenticateToken.Instance.UserName);
            }
            //var match = JsonParser.GetMatchById(region, matchId);
            var build = JsonParser.GetBuild(region, playerName, Convert.ToInt64(matchId));


            dbm.AddBuild(user,build);
            //db.Build.Add(build);
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = build.BuildId }, build);
        }

        // DELETE: api/Builds/5
        [ResponseType(typeof(Build))]
        public IHttpActionResult DeleteBuild(int id)
        {
            Build build = db.Build.Find(id);
            if (build == null)
            {
                return NotFound();
            }

            db.Build.Remove(build);
            db.SaveChanges();

            return Ok(build);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuildExists(int id)
        {
            return db.Build.Count(e => e.BuildId == id) > 0;
        }
    }
}