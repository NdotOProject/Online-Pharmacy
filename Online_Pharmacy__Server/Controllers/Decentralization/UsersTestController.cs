//
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Models.Decentralization;
using OnlinePharmacy.DTO.Mappers.Decentralization;
using OnlinePharmacy.Repositories.Decentralization;

namespace Online_Pharmacy__Server.Controllers
{
    //
    [RoutePrefix(AppConfig.InternalApiPrefix + "users")]
    public class UsersTestController : ApiController
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();
        private readonly UserMapper userMapper = new UserMapper();
        private readonly UserRepository userRepos = new UserRepository();

        // GET: api/internal/users
        [Route("")]
        public ICollection<Users> GetUsers()
        {
            var users = db.Users;
            var list = new List<UserDTO>();

            /*foreach (var user in users)
            {
                //list.Add(userMapper.ToDTO(user));
            }*/

            return db.Users.ToList();
        }

        // GET: api/internal/users/1
        [Route("{id:int}")]
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult GetUsers(int id)
        {
            UserDTO users = userRepos.GetUser(id);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // PUT: 
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsers(int id, UserDTO dto)
        {
            //var users = userMapper.ToObject(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.SimpleInformation.ID)
            {
                return BadRequest();
            }

            //db.Entry(users).State = EntityState.Modified;

            try
            {
              //  db.SaveChanges();
                if (userRepos.UpdateUser(dto))
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: 
        [Route("", Name = "PostUsers")]
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult PostUsers(UserDTO dto)
        {
            var users = userMapper.ToObject(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(users);
            db.SaveChanges();

            return CreatedAtRoute("PostUsers", new { id = dto.SimpleInformation.ID }, dto);
        }

        // DELETE: api/internal/users/1
        [Route("{id:int}")]
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult DeleteUsers(int id)
        {
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }

            users.Status = false;
            //db.Users.Remove(users);
            db.SaveChanges();

            return Ok(userMapper.ToDTO(users));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsersExists(int id)
        {
            return db.Users.Count(e => e.ID == id) > 0;
        }
    }
}