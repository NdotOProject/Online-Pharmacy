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
using Online_Pharmacy__Server.Models;

namespace Online_Pharmacy__Server.Controllers.Recruiment
{
    public class RecruimentNewsController : ApiController
    {
        private OnlinePharmacyEntities db = new OnlinePharmacyEntities();

        // GET: api/RecruimentNews
        public IQueryable<RecruimentNews> GetRecruimentNews()
        {
            return db.RecruimentNews;
        }

        // GET: api/RecruimentNews/5
        [ResponseType(typeof(RecruimentNews))]
        public IHttpActionResult GetRecruimentNews(int id)
        {
            RecruimentNews recruimentNews = db.RecruimentNews.Find(id);
            if (recruimentNews == null)
            {
                return NotFound();
            }

            return Ok(recruimentNews);
        }

        // PUT: api/RecruimentNews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecruimentNews(int id, RecruimentNews recruimentNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recruimentNews.ID)
            {
                return BadRequest();
            }

            db.Entry(recruimentNews).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecruimentNewsExists(id))
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

        // POST: api/RecruimentNews
        [ResponseType(typeof(RecruimentNews))]
        public IHttpActionResult PostRecruimentNews(RecruimentNews recruimentNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RecruimentNews.Add(recruimentNews);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = recruimentNews.ID }, recruimentNews);
        }

        // DELETE: api/RecruimentNews/5
        [ResponseType(typeof(RecruimentNews))]
        public IHttpActionResult DeleteRecruimentNews(int id)
        {
            RecruimentNews recruimentNews = db.RecruimentNews.Find(id);
            if (recruimentNews == null)
            {
                return NotFound();
            }

            db.RecruimentNews.Remove(recruimentNews);
            db.SaveChanges();

            return Ok(recruimentNews);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecruimentNewsExists(int id)
        {
            return db.RecruimentNews.Count(e => e.ID == id) > 0;
        }
    }
}