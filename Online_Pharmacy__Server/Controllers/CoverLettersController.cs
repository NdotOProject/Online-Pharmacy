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
using OnlinePharmacy.DTO.Recruiment;

namespace Online_Pharmacy__Server.Controllers
{
    public class CoverLettersController : ApiController
    {
        private OnlinePharmacyEntities db = new OnlinePharmacyEntities();
        //private CoverLetterMapper coverLetterMapper = new CoverLetterMapper();

        // GET: api/CoverLetters
        public ICollection<CoverLetterDTO> GetCoverLetters()
        {
            var letters = db.CoverLetters;
            var list = new List<CoverLetterDTO>();

            foreach (var letter in letters)
            {
                //list.Add();
            }

            return list;
        }

        // GET: api/CoverLetters/5
        [ResponseType(typeof(CoverLetters))]
        public IHttpActionResult GetCoverLetters(int id)
        {
            CoverLetters coverLetters = db.CoverLetters.Find(id);
            if (coverLetters == null)
            {
                return NotFound();
            }

            return Ok(coverLetters);
        }

        // PUT: api/CoverLetters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoverLetters(int id, CoverLetters coverLetters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coverLetters.ID)
            {
                return BadRequest();
            }

            db.Entry(coverLetters).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoverLettersExists(id))
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

        // POST: api/CoverLetters
        [ResponseType(typeof(CoverLetters))]
        public IHttpActionResult PostCoverLetters(CoverLetters coverLetters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoverLetters.Add(coverLetters);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = coverLetters.ID }, coverLetters);
        }

        // DELETE: api/CoverLetters/5
        [ResponseType(typeof(CoverLetters))]
        public IHttpActionResult DeleteCoverLetters(int id)
        {
            CoverLetters coverLetters = db.CoverLetters.Find(id);
            if (coverLetters == null)
            {
                return NotFound();
            }

            db.CoverLetters.Remove(coverLetters);
            db.SaveChanges();

            return Ok(coverLetters);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoverLettersExists(int id)
        {
            return db.CoverLetters.Count(e => e.ID == id) > 0;
        }
    }
}