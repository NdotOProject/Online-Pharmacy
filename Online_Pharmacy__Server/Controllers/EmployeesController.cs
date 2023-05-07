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
using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Internal;
using OnlinePharmacy.DTO.Mappers;

namespace Online_Pharmacy__Server.Controllers
{
    //
    [RoutePrefix(AppConfig.InternalApiPrefix + "employees")]
    public class EmployeesController : ApiController
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();
        private readonly EmployeeMapper empMapper = new EmployeeMapper();

        // GET: api/internal/employees
        [Route("")]
        public ICollection<EmployeeDTO> GetEmployees()
        {
            var employees = db.Employees;
            var list = new List<EmployeeDTO>();
            foreach (var emp in employees)
            {
                list.Add(empMapper.ToDTO(emp));
            }
            return list;
        }

        // GET: api/internal/employees/1
        [Route("{id:int}")]
        [ResponseType(typeof(EmployeeDTO))]
        public IHttpActionResult GetEmployees(int id)
        {
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return NotFound();
            }

            return Ok(empMapper.ToDTO(employees));
        }

        // PUT: 
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployees(int id, EmployeeDTO dto)
        {
            var employees = empMapper.ToObject(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employees.ID)
            {
                return BadRequest();
            }

            db.Entry(employees).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesExists(id))
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
        [Route("", Name = "PostEmployees")]
        [ResponseType(typeof(EmployeeDTO))]
        public IHttpActionResult PostEmployees(EmployeeDTO dto)
        {
            var employees = empMapper.ToObject(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employees);
            db.SaveChanges();

            return CreatedAtRoute("PostEmployees", new { id = dto.ID }, dto);
        }

        // DELETE: api/internal/employees/1
        [Route("{id:int}")]
        [ResponseType(typeof(EmployeeDTO))]
        public IHttpActionResult DeleteEmployees(int id)
        {
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return NotFound();
            }

            employees.Status = false;
            //db.Employees.Remove(employees);
            db.SaveChanges();

            return Ok(empMapper.ToDTO(employees));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeesExists(int id)
        {
            return db.Employees.Count(e => e.ID == id) > 0;
        }
    }
}