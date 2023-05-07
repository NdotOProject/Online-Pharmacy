using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Internal;
using OnlinePharmacy.Mappers;

namespace Online_Pharmacy__Server.Controllers
{
    // complete
    [RoutePrefix(AppConfig.InternalApiPrefix + "organizations")]
    public class OrganizationsController : ApiController
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();
        private readonly OrganizationMapper orgMapper = new OrganizationMapper();

        // GET: 
        [Route("")]
        public ICollection<OrganizationDTO> GetOrganizations()
        {
            var orgs = db.Organizations;
            var list = new List<OrganizationDTO>();
            foreach (var org in orgs)
            {
                list.Add(orgMapper.ToDTO(org));
            }
            return list;
        }

        // GET: 
        [Route("{id:int}")]
        [ResponseType(typeof(OrganizationDTO))]
        public IHttpActionResult GetOrganizations(int id)
        {
            Organizations organizations = db.Organizations.Find(id);
            if (organizations == null)
            {
                return NotFound();
            }

            return Ok(orgMapper.ToDTO(organizations));
        }

        // PUT: 
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrganizations(int id, OrganizationDTO dto)
        {
            var organizations = orgMapper.ToObject(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organizations.ID)
            {
                return BadRequest();
            }

            db.Entry(organizations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationsExists(id))
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

        // POST: 
        [Route("", Name = "PostOrganizations")]
        [ResponseType(typeof(OrganizationDTO))]
        public IHttpActionResult PostOrganizations(OrganizationDTO dto)
        {
            var organizations = orgMapper.ToObject(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Organizations.Add(organizations);
            db.SaveChanges();

            return CreatedAtRoute("PostOrganizations", new { id = dto.ID }, dto);
        }

        // DELETE: 
        [Route("{id:int}")]
        [ResponseType(typeof(OrganizationDTO))]
        public IHttpActionResult DeleteOrganizations(int id)
        {
            Organizations organizations = db.Organizations.Find(id);
            if (organizations == null)
            {
                return NotFound();
            }

            db.Organizations.Remove(organizations);
            db.SaveChanges();

            return Ok(orgMapper.ToDTO(organizations));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationsExists(int id)
        {
            return db.Organizations.Count(e => e.ID == id) > 0;
        }
    }
}