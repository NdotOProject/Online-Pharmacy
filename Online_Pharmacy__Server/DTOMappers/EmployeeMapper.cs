using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.Repositories;
using OnlinePharmacy.DTO.Decentralization;
using OnlinePharmacy.DTO.Internal;
using OnlinePharmacy.Mappers.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlinePharmacy.DTO.Mappers
{
    public class EmployeeMapper : IMapper<Employees, EmployeeDTO>
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();

        


        public EmployeeDTO ToDTO(Employees obj)
        {
            EmployeeDTO dto = new EmployeeDTO
            {
                /*ID = obj.ID,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Email = obj.Email,
                Phone = obj.Phone,
                Address = obj.Address,
                UserName = obj.UserName,
                Password = obj.Password,
                Status = obj.Status*/
            };
            //
            /*dto.FullName = dto.FirstName + " " + dto.LastName;
            //
            var gender = db.Gender
                           .Where(g => g.ID == obj.GenderID)
                           .ToList()[0];
            dto.Gender = new GenderMapper().ToDTO(gender);
            //
            var org = db.Organizations
                        .Where(o => o.ID == obj.OrgID)
                        .ToList()[0];
            dto.Organization = new SubOrganizationMapper().ToDTO(org);
            //
            var bosses = db.Employees
                         .Where(e => e.ID == obj.ReportTo)
                         .ToList();
            var boss = bosses.Count > 0 ? bosses[0] : null;
            dto.Boss = new SubEmployeeMapper().ToDTO(boss);

            dto.DoFunctions = GetFunctions(dto.ID);*/
            
            return dto;
        }

        public Employees ToObject(EmployeeDTO dto)
        {
            Employees obj = new Employees();
            //
            /*obj.ID = dto.ID;
            obj.FirstName = dto.FirstName;
            obj.LastName = dto.LastName;
            obj.Email = dto.Email;
            obj.Phone = dto.Phone;
            obj.Address = dto.Address;
            obj.UserName = dto.UserName;
            obj.Password = dto.Password;
            obj.Status = dto.Status;
            //
            obj.GenderID = dto.Gender.ID;
            obj.OrgID = dto.Organization.ID;
            if (dto.Boss != null)
            {
                obj.ReportTo = dto.Boss.ID;
            }*/
            return obj;
        }
    }

    public class SubEmployeeMapper : IMapper<Employees, SubEmployeeDTO>
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();

        public SubEmployeeDTO ToDTO(Employees obj)
        {
            if (obj == null) { return null; }
            return new SubEmployeeDTO
            {
                ID= obj.ID,
                //FullName = obj.FirstName + " " + obj.LastName,
            };
        }

        public Employees ToObject(SubEmployeeDTO dto)
        {
            return db.Employees.Where(e => e.ID == dto.ID).ToList()[0];
        }
    }
}