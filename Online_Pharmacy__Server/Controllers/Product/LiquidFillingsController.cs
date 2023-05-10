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
    public class LiquidFillingsController : ApiController
    {
        private OnlinePharmacyEntities db = new OnlinePharmacyEntities();

        // GET: api/LiquidFillings
        public IQueryable<LiquidFillings> GetLiquidFillings()
        {
            return db.LiquidFillings;
        }

        // GET: api/LiquidFillings/5
        [ResponseType(typeof(LiquidFillings))]
        public IHttpActionResult GetLiquidFillings(int id)
        {
            LiquidFillings liquidFillings = db.LiquidFillings.Find(id);
            if (liquidFillings == null)
            {
                return NotFound();
            }

            return Ok(liquidFillings);
        }

        // PUT: api/LiquidFillings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLiquidFillings(int id, LiquidFillings liquidFillings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != liquidFillings.ProductID)
            {
                return BadRequest();
            }

            db.Entry(liquidFillings).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LiquidFillingsExists(id))
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

        // POST: api/LiquidFillings
        [ResponseType(typeof(LiquidFillings))]
        public IHttpActionResult PostLiquidFillings(LiquidFillings liquidFillings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LiquidFillings.Add(liquidFillings);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LiquidFillingsExists(liquidFillings.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = liquidFillings.ProductID }, liquidFillings);
        }

        // DELETE: api/LiquidFillings/5
        [ResponseType(typeof(LiquidFillings))]
        public IHttpActionResult DeleteLiquidFillings(int id)
        {
            LiquidFillings liquidFillings = db.LiquidFillings.Find(id);
            if (liquidFillings == null)
            {
                return NotFound();
            }

            db.LiquidFillings.Remove(liquidFillings);
            db.SaveChanges();

            return Ok(liquidFillings);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LiquidFillingsExists(int id)
        {
            return db.LiquidFillings.Count(e => e.ProductID == id) > 0;
        }
    }
}