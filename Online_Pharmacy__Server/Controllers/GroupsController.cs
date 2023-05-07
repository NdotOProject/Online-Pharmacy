using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Decentralization;
using OnlinePharmacy.Mappers.Decentralization;
using OnlinePharmacy.Repositories.Decentralization;

namespace Online_Pharmacy__Server.Controllers
{
    // complete
    [RoutePrefix(AppConfig.InternalApiPrefix + "groups")]
    public class GroupsController : ApiController
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();
        private readonly GroupRepository groupRepos = new GroupRepository();
        private readonly GroupMapper groupMapper = new GroupMapper();

        // GET: api/internal/groups
        [Route("")]
        public ICollection<GroupDTO> GetGroups()
        {
            var groups = db.Groups;
            var list = new List<GroupDTO>();

            foreach (var group in groups)
            {
                list.Add(groupMapper.ToDTO(group));
            }

            return list;
        }

        // GET: api/internal/groups/1
        [Route("{id:int}")]
        [ResponseType(typeof(GroupDTO))]
        public IHttpActionResult GetGroups(int id)
        {
            Groups groups = db.Groups.Find(id);
            if (groups == null)
            {
                return NotFound();
            }

            return Ok(groupMapper.ToDTO(groups));
        }

        // PUT: api/internal/groups/1
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGroups(int id, GroupDTO dto)
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
                if (groupRepos.UpdateGroup(dto))
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupsExists(id))
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

        // POST: api/internal/groups
        [Route("", Name = "PostGroups")]
        [ResponseType(typeof(GroupDTO))]
        public IHttpActionResult PostGroups(GroupDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = groupRepos.CreateGroup(dto);
            if (result != null)
            {
                return CreatedAtRoute("PostGroups", new { id = result.ID }, result);
            }

            return BadRequest();
        }

        // DELETE: api/internal/groups/1
        [Route("{id:int}")]
        [ResponseType(typeof(GroupDTO))]
        public IHttpActionResult DeleteGroups(int id)
        {
            GroupDTO groups = groupRepos.DeleteGroup(id);
            if (groups == null)
            {
                return NotFound();
            }

            return Ok(groups);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupsExists(int id)
        {
            return db.Groups.Count(e => e.ID == id) > 0;
        }
    }
}