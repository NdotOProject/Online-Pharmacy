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
    public class ProductImagesController : ApiController
    {
        private OnlinePharmacyEntities db = new OnlinePharmacyEntities();

        // GET: api/ProductImages
        public IQueryable<ProductImages> GetProductImages()
        {
            return db.ProductImages;
        }

        // GET: api/ProductImages/5
        [ResponseType(typeof(ProductImages))]
        public IHttpActionResult GetProductImages(int id)
        {
            ProductImages productImages = db.ProductImages.Find(id);
            if (productImages == null)
            {
                return NotFound();
            }

            return Ok(productImages);
        }

        // PUT: api/ProductImages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductImages(int id, ProductImages productImages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productImages.ID)
            {
                return BadRequest();
            }

            db.Entry(productImages).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductImagesExists(id))
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

        // POST: api/ProductImages
        [ResponseType(typeof(ProductImages))]
        public IHttpActionResult PostProductImages(ProductImages productImages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductImages.Add(productImages);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productImages.ID }, productImages);
        }

        // DELETE: api/ProductImages/5
        [ResponseType(typeof(ProductImages))]
        public IHttpActionResult DeleteProductImages(int id)
        {
            ProductImages productImages = db.ProductImages.Find(id);
            if (productImages == null)
            {
                return NotFound();
            }

            db.ProductImages.Remove(productImages);
            db.SaveChanges();

            return Ok(productImages);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductImagesExists(int id)
        {
            return db.ProductImages.Count(e => e.ID == id) > 0;
        }
    }
}