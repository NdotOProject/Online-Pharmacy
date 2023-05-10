using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Mappers.Generic;
using OnlinePharmacy.DTO.Models.User;
using OnlinePharmacy.Mappers.Generic;
using OnlinePharmacy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlinePharmacy.DTO.Mappers.User
{
    public class EmployeeMapper : IMapper<Employees, EmployeeDTO>
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();
        private readonly UserInfoMapper userInfoMapper = new UserInfoMapper();
        private readonly EmployeeRepository empRepos = new EmployeeRepository();
        public EmployeeDTO ToDTO(Employees obj)
        {
            UserInfoDTO userInfo = userInfoMapper.ToDTO(obj);

            //new SubOrganizationMapper();
            int empId = obj.ID;
            EmployeeDTO dto = new EmployeeDTO
            {
                ID = empId,
                Information = userInfo,
                Organization = empRepos.GetOrganization(empId),
                Boss = empRepos.GetBoss(empId),
                Users = empRepos.GetUsers(empId),
                ImplementFunctions = null,
            };
            /*
            var org = db.Organizations
                        .Where(o => o.ID == obj.OrgID)
                        .ToList()[0];
            dto.Organization = .ToDTO(org);
            //
            var bosses = db.Employees
                         .Where(e => e.ID == obj.ReportTo)
                         .ToList();
            var boss = bosses.Count > 0 ? bosses[0] : null;
            dto.Boss = new SubEmployeeMapper().ToDTO(boss);
            */

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

    #region SubEmployeeMapper: completed
    public class SubEmployeeMapper : IMapper<Employees, SubEmployeeDTO>
    {
        public SubEmployeeDTO ToDTO(Employees obj)
        {
            if (obj == null) { return null; }
            return new SubEmployeeDTO
            {
                ID = obj.ID,
                FullName = new UserInfoMapper().ToDTO(obj).FullName,
            };
        }

        public Employees ToObject(SubEmployeeDTO dto)
        {
            return AppConfig.DefaultDatabase().Employees.Find(dto.ID);
        }

    }
    #endregion

}