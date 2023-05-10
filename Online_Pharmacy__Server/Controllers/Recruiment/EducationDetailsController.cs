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

namespace Online_Pharmacy__Server.Controllers
{
    public class EducationDetailsController : ApiController
    {
        private OnlinePharmacyEntities db = new OnlinePharmacyEntities();

        // GET: api/EducationDetails
        public IQueryable<EducationDetails> GetEducationDetails()
        {
            return db.EducationDetails;
        }

        // GET: api/EducationDetails/5
        [ResponseType(typeof(EducationDetails))]
        public IHttpActionResult GetEducationDetails(int id)
        {
            EducationDetails educationDetails = db.EducationDetails.Find(id);
            if (educationDetails == null)
            {
                return NotFound();
            }

            return Ok(educationDetails);
        }

        // PUT: api/EducationDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEducationDetails(int id, EducationDetails educationDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != educationDetails.LetterID)
            {
                return BadRequest();
            }

            db.Entry(educationDetails).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationDetailsExists(id))
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

        // POST: api/EducationDetails
        [ResponseType(typeof(EducationDetails))]
        public IHttpActionResult PostEducationDetails(EducationDetails educationDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EducationDetails.Add(educationDetails);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EducationDetailsExists(educationDetails.LetterID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = educationDetails.LetterID }, educationDetails);
        }

        // DELETE: api/EducationDetails/5
        [ResponseType(typeof(EducationDetails))]
        public IHttpActionResult DeleteEducationDetails(int id)
        {
            EducationDetails educationDetails = db.EducationDetails.Find(id);
            if (educationDetails == null)
            {
                return NotFound();
            }

            db.EducationDetails.Remove(educationDetails);
            db.SaveChanges();

            return Ok(educationDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EducationDetailsExists(int id)
        {
            return db.EducationDetails.Count(e => e.LetterID == id) > 0;
        }
    }
}