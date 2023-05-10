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
    public class FeedbackRecruimentsController : ApiController
    {
        private OnlinePharmacyEntities db = new OnlinePharmacyEntities();

        // GET: api/FeedbackRecruiments
        public IQueryable<FeedbackRecruiment> GetFeedbackRecruiment()
        {
            return db.FeedbackRecruiment;
        }

        // GET: api/FeedbackRecruiments/5
        [ResponseType(typeof(FeedbackRecruiment))]
        public IHttpActionResult GetFeedbackRecruiment(int id)
        {
            FeedbackRecruiment feedbackRecruiment = db.FeedbackRecruiment.Find(id);
            if (feedbackRecruiment == null)
            {
                return NotFound();
            }

            return Ok(feedbackRecruiment);
        }

        // PUT: api/FeedbackRecruiments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFeedbackRecruiment(int id, FeedbackRecruiment feedbackRecruiment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feedbackRecruiment.ID)
            {
                return BadRequest();
            }

            db.Entry(feedbackRecruiment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackRecruimentExists(id))
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

        // POST: api/FeedbackRecruiments
        [ResponseType(typeof(FeedbackRecruiment))]
        public IHttpActionResult PostFeedbackRecruiment(FeedbackRecruiment feedbackRecruiment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FeedbackRecruiment.Add(feedbackRecruiment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = feedbackRecruiment.ID }, feedbackRecruiment);
        }

        // DELETE: api/FeedbackRecruiments/5
        [ResponseType(typeof(FeedbackRecruiment))]
        public IHttpActionResult DeleteFeedbackRecruiment(int id)
        {
            FeedbackRecruiment feedbackRecruiment = db.FeedbackRecruiment.Find(id);
            if (feedbackRecruiment == null)
            {
                return NotFound();
            }

            db.FeedbackRecruiment.Remove(feedbackRecruiment);
            db.SaveChanges();

            return Ok(feedbackRecruiment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeedbackRecruimentExists(int id)
        {
            return db.FeedbackRecruiment.Count(e => e.ID == id) > 0;
        }
    }
}