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
    public class TabletsController : ApiController
    {
        private OnlinePharmacyEntities db = new OnlinePharmacyEntities();

        // GET: api/Tablets
        public IQueryable<Tablets> GetTablets()
        {
            return db.Tablets;
        }

        // GET: api/Tablets/5
        [ResponseType(typeof(Tablets))]
        public IHttpActionResult GetTablets(int id)
        {
            Tablets tablets = db.Tablets.Find(id);
            if (tablets == null)
            {
                return NotFound();
            }

            return Ok(tablets);
        }

        // PUT: api/Tablets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTablets(int id, Tablets tablets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tablets.ProductID)
            {
                return BadRequest();
            }

            db.Entry(tablets).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TabletsExists(id))
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

        // POST: api/Tablets
        [ResponseType(typeof(Tablets))]
        public IHttpActionResult PostTablets(Tablets tablets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tablets.Add(tablets);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TabletsExists(tablets.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tablets.ProductID }, tablets);
        }

        // DELETE: api/Tablets/5
        [ResponseType(typeof(Tablets))]
        public IHttpActionResult DeleteTablets(int id)
        {
            Tablets tablets = db.Tablets.Find(id);
            if (tablets == null)
            {
                return NotFound();
            }

            db.Tablets.Remove(tablets);
            db.SaveChanges();

            return Ok(tablets);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TabletsExists(int id)
        {
            return db.Tablets.Count(e => e.ProductID == id) > 0;
        }
    }
}