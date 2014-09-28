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
    public class SongArtistController : ApiController
    {
        private TeluguLyricsDbEntities db = new TeluguLyricsDbEntities();

        // GET api/SongArtist
        public IEnumerable<SongArtist> GetSongArtists()
        {
            var songartists = db.SongArtists.Include(s => s.Artist).Include(s => s.Song);
            return songartists.AsEnumerable();
        }

        // GET api/SongArtist/5
        public SongArtist GetSongArtist(int id)
        {
            SongArtist songartist = db.SongArtists.Find(id);
            if (songartist == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return songartist;
        }

        // PUT api/SongArtist/5
        public HttpResponseMessage PutSongArtist(int id, SongArtist songartist)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != songartist.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(songartist).State = EntityState.Modified;

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

        // POST api/SongArtist
        public HttpResponseMessage PostSongArtist(SongArtist songartist)
        {
            if (ModelState.IsValid)
            {
                db.SongArtists.Add(songartist);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, songartist);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = songartist.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/SongArtist/5
        public HttpResponseMessage DeleteSongArtist(int id)
        {
            SongArtist songartist = db.SongArtists.Find(id);
            if (songartist == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.SongArtists.Remove(songartist);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, songartist);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}