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
    public class ProductTypesController : ApiController
    {
        private OnlinePharmacyEntities db = new OnlinePharmacyEntities();

        // GET: api/ProductTypes
        public IQueryable<ProductTypes> GetProductTypes()
        {
            return db.ProductTypes;
        }

        // GET: api/ProductTypes/5
        [ResponseType(typeof(ProductTypes))]
        public IHttpActionResult GetProductTypes(int id)
        {
            ProductTypes productTypes = db.ProductTypes.Find(id);
            if (productTypes == null)
            {
                return NotFound();
            }

            return Ok(productTypes);
        }

        // PUT: api/ProductTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductTypes(int id, ProductTypes productTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productTypes.ID)
            {
                return BadRequest();
            }

            db.Entry(productTypes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypesExists(id))
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

        // POST: api/ProductTypes
        [ResponseType(typeof(ProductTypes))]
        public IHttpActionResult PostProductTypes(ProductTypes productTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductTypes.Add(productTypes);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productTypes.ID }, productTypes);
        }

        // DELETE: api/ProductTypes/5
        [ResponseType(typeof(ProductTypes))]
        public IHttpActionResult DeleteProductTypes(int id)
        {
            ProductTypes productTypes = db.ProductTypes.Find(id);
            if (productTypes == null)
            {
                return NotFound();
            }

            db.ProductTypes.Remove(productTypes);
            db.SaveChanges();

            return Ok(productTypes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductTypesExists(int id)
        {
            return db.ProductTypes.Count(e => e.ID == id) > 0;
        }
    }
}