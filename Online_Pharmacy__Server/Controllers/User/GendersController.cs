using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.Mappers.Generic;
using OnlinePharmacy.DTO.Models.User;

namespace Online_Pharmacy__Server.Controllers
{
    // complete
    [RoutePrefix(AppConfig.PublishApiPrefix + "genders")]
    public class GendersController : ApiController
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();
        private readonly GenderMapper genderMapper = new GenderMapper();

        [Route("")]
        public ICollection<GenderDTO> GetGender()
        {
            var genders = db.Gender;
            var list = new List<GenderDTO>();
            foreach (var gender in genders)
            {
                list.Add(genderMapper.ToDTO(gender));
            }
            return list;
        }

        [Route("{id:int}")]
        [ResponseType(typeof(GenderDTO))]
        public IHttpActionResult GetGender(int id)
        {
            Gender gender = db.Gender.Find(id);
            if (gender == null)
            {
                return NotFound();
            }

            return Ok(genderMapper.ToDTO(gender));
        }

        /*
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGender(int id, GenderDTO dto)
        {
            var gender = mapper.ToObject(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gender.ID)
            {
                return BadRequest();
            }

            db.Entry(gender).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderExists(id))
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

        [Route("")]
        [ResponseType(typeof(GenderDTO))]
        public IHttpActionResult PostGender(GenderDTO dto)
        {
            var gender = mapper.ToObject(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gender.Add(gender);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GenderExists(gender.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dto.ID }, dto);
        }

        [Route("{id:int}")]
        [ResponseType(typeof(GenderDTO))]
        public IHttpActionResult DeleteGender(int id)
        {
            Gender gender = db.Gender.Find(id);
            if (gender == null)
            {
                return NotFound();
            }

            db.Gender.Remove(gender);
            db.SaveChanges();

            return Ok(mapper.ToDTO(gender));
        }
        */

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GenderExists(int id)
        {
            return db.Gender.Count(e => e.ID == id) > 0;
        }
    }
}