using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TeluguLyricsWebApi.Models;

namespace TeluguLyricsWebApi.Controllers
{
    public class LyricistController : ApiController
    {
        private TeluguLyricsDbEntities db = new TeluguLyricsDbEntities();

        // GET api/Lyricist
        public IEnumerable<Lyricist> GetLyricists()
        {
            return db.Lyricists.AsEnumerable();
        }

        // GET api/Lyricist/5
        public Lyricist GetLyricist(int id)
        {
            Lyricist lyricist = db.Lyricists.Find(id);
            if (lyricist == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return lyricist;
        }

        // PUT api/Lyricist/5
        public HttpResponseMessage PutLyricist(int id, Lyricist lyricist)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != lyricist.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(lyricist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Lyricist
        public HttpResponseMessage PostLyricist(Lyricist lyricist)
        {
            if (ModelState.IsValid)
            {
                db.Lyricists.Add(lyricist);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, lyricist);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = lyricist.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Lyricist/5
        public HttpResponseMessage DeleteLyricist(int id)
        {
            Lyricist lyricist = db.Lyricists.Find(id);
            if (lyricist == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Lyricists.Remove(lyricist);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, lyricist);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}