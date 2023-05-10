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
    public class FeedbackProductsController : ApiController
    {
        private OnlinePharmacyEntities db = new OnlinePharmacyEntities();

        // GET: api/FeedbackProducts
        public IQueryable<FeedbackProduct> GetFeedbackProduct()
        {
            return db.FeedbackProduct;
        }

        // GET: api/FeedbackProducts/5
        [ResponseType(typeof(FeedbackProduct))]
        public IHttpActionResult GetFeedbackProduct(int id)
        {
            FeedbackProduct feedbackProduct = db.FeedbackProduct.Find(id);
            if (feedbackProduct == null)
            {
                return NotFound();
            }

            return Ok(feedbackProduct);
        }

        // PUT: api/FeedbackProducts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFeedbackProduct(int id, FeedbackProduct feedbackProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feedbackProduct.ID)
            {
                return BadRequest();
            }

            db.Entry(feedbackProduct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackProductExists(id))
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

        // POST: api/FeedbackProducts
        [ResponseType(typeof(FeedbackProduct))]
        public IHttpActionResult PostFeedbackProduct(FeedbackProduct feedbackProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FeedbackProduct.Add(feedbackProduct);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = feedbackProduct.ID }, feedbackProduct);
        }

        // DELETE: api/FeedbackProducts/5
        [ResponseType(typeof(FeedbackProduct))]
        public IHttpActionResult DeleteFeedbackProduct(int id)
        {
            FeedbackProduct feedbackProduct = db.FeedbackProduct.Find(id);
            if (feedbackProduct == null)
            {
                return NotFound();
            }

            db.FeedbackProduct.Remove(feedbackProduct);
            db.SaveChanges();

            return Ok(feedbackProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeedbackProductExists(int id)
        {
            return db.FeedbackProduct.Count(e => e.ID == id) > 0;
        }
    }
}