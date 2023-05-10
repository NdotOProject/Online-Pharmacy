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

namespace Online_Pharmacy__Server.Controllers.Product
{
    public class EncapsulationsController : ApiController
    {
        private OnlinePharmacyEntities db = new OnlinePharmacyEntities();

        // GET: api/Encapsulations
        public IQueryable<Encapsulations> GetEncapsulations()
        {
            return db.Encapsulations;
        }

        // GET: api/Encapsulations/5
        [ResponseType(typeof(Encapsulations))]
        public IHttpActionResult GetEncapsulations(int id)
        {
            Encapsulations encapsulations = db.Encapsulations.Find(id);
            if (encapsulations == null)
            {
                return NotFound();
            }

            return Ok(encapsulations);
        }

        // PUT: api/Encapsulations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEncapsulations(int id, Encapsulations encapsulations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != encapsulations.ProductID)
            {
                return BadRequest();
            }

            db.Entry(encapsulations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncapsulationsExists(id))
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

        // POST: api/Encapsulations
        [ResponseType(typeof(Encapsulations))]
        public IHttpActionResult PostEncapsulations(Encapsulations encapsulations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Encapsulations.Add(encapsulations);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EncapsulationsExists(encapsulations.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = encapsulations.ProductID }, encapsulations);
        }

        // DELETE: api/Encapsulations/5
        [ResponseType(typeof(Encapsulations))]
        public IHttpActionResult DeleteEncapsulations(int id)
        {
            Encapsulations encapsulations = db.Encapsulations.Find(id);
            if (encapsulations == null)
            {
                return NotFound();
            }

            db.Encapsulations.Remove(encapsulations);
            db.SaveChanges();

            return Ok(encapsulations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EncapsulationsExists(int id)
        {
            return db.Encapsulations.Count(e => e.ProductID == id) > 0;
        }
    }
}