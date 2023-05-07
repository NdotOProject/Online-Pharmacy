using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Decentralization;
using OnlinePharmacy.Repositories.Decentralization;

//
namespace Online_Pharmacy__Server.Controllers
{
    // complete
    [RoutePrefix(AppConfig.InternalApiPrefix + "functions")]
    public class FunctionsController : ApiController
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();
        private readonly FunctionRepository funcRepos = new FunctionRepository();

        // GET: api/internal/functions
        [Route("")]
        public ICollection<FunctionDTO> GetFunctions()
        {
            return funcRepos.GetFunctions();
        }

        // GET: api/internal/functions/1
        [Route("{id:int}")]
        [ResponseType(typeof(FunctionDTO))]
        public IHttpActionResult GetFunctions(int id)
        {
            var function = funcRepos.GetFunction(id);

            if (function == null)
            {
                return NotFound();
            }

            return Ok(function);
        }

        // PUT: api/internal/functions/1
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFunctions(int id, FunctionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.ID)
            {
                return BadRequest();
            }

            try
            {
                if (funcRepos.UpdateFunction(dto))
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FunctionsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return BadRequest();
        }

        // POST: api/internal/functions
        [Route("", Name = "PostFunctions")]
        [ResponseType(typeof(FunctionDTO))]
        public IHttpActionResult PostFunctions(FunctionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (funcRepos.CreateFuncion(dto) > 0)
            {
                return CreatedAtRoute("PostFunctions", new { id = dto.ID }, dto);
            }

            return BadRequest();
        }

        // DELETE: api/internal/functions/1
        [Route("{id:int}")]
        [ResponseType(typeof(FunctionDTO))]
        public IHttpActionResult DeleteFunctions(int id)
        {
            var func = funcRepos.DeleteFunction(id);
            if (func != null)
            {
                return Ok(func);
            }
            else
            {
                return NotFound();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FunctionsExists(int id)
        {
            return db.Functions.Count(e => e.ID == id) > 0;
        }
    
    }
}