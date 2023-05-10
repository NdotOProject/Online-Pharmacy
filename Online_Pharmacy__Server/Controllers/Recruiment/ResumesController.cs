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
    public class ResumesController : ApiController
    {
        private OnlinePharmacyEntities db = new OnlinePharmacyEntities();

        // GET: api/Resumes
        public IQueryable<Resume> GetResume()
        {
            return db.Resume;
        }

        // GET: api/Resumes/5
        [ResponseType(typeof(Resume))]
        public IHttpActionResult GetResume(int id)
        {
            Resume resume = db.Resume.Find(id);
            if (resume == null)
            {
                return NotFound();
            }

            return Ok(resume);
        }

        // PUT: api/Resumes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResume(int id, Resume resume)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resume.LetterID)
            {
                return BadRequest();
            }

            db.Entry(resume).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResumeExists(id))
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

        // POST: api/Resumes
        [ResponseType(typeof(Resume))]
        public IHttpActionResult PostResume(Resume resume)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Resume.Add(resume);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ResumeExists(resume.LetterID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = resume.LetterID }, resume);
        }

        // DELETE: api/Resumes/5
        [ResponseType(typeof(Resume))]
        public IHttpActionResult DeleteResume(int id)
        {
            Resume resume = db.Resume.Find(id);
            if (resume == null)
            {
                return NotFound();
            }

            db.Resume.Remove(resume);
            db.SaveChanges();

            return Ok(resume);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResumeExists(int id)
        {
            return db.Resume.Count(e => e.LetterID == id) > 0;
        }
    }
}