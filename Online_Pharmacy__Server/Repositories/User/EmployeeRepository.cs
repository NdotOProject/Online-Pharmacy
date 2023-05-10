using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Models.Decentralization;
using OnlinePharmacy.DTO.Mappers.Decentralization;
using OnlinePharmacy.Repositories.Decentralization;
using OnlinePharmacy.DTO.Models.User;
using Online_Pharmacy__Server.App_Start;
using OnlinePharmacy.DTO.Mappers.User;

namespace OnlinePharmacy.Repositories
{
    public class EmployeeRepository : BaseRepository
    {

        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();
        private readonly SubEmployeeMapper subEmpMapper = new SubEmployeeMapper();
        private readonly SubOrganizationMapper subOrgMapper = new SubOrganizationMapper();

        public SubOrganizationDTO GetOrganization(int empId)
        {
            Employees emp = db.Employees.Find(empId);
            Organizations org = db.Organizations.Find(emp.OrgID);
            return subOrgMapper.ToDTO(org);
        }

        public SubEmployeeDTO GetBoss(int empId)
        {
            Employees emp = db.Employees.Find(empId);
            if (emp.ReportTo != null)
            {
                Employees boss = db.Employees.Find(emp.ReportTo);
                return subEmpMapper.ToDTO(boss);
            }
            return null;
        }

        public ICollection<SubUserDTO> GetUsers(int EmpID)
        {
            var users = new List<SubUserDTO>();

            string sql = "select * from Users" +
                        " where EmpID=@EmpID";
            SqlParameter[] parameters =
            {
                new SqlParameter("@EmpID", EmpID)
            };

            if (ExecuteQuery(sql, parameters, out DataRowCollection rows))
            {
                var mapper = new SubUserMapper();
                foreach (DataRow row in rows)
                {
                    var user = new Users
                    {
                        ID = int.Parse(row["ID"].ToString()),
                        Name = row["Name"].ToString(),
                        Description = row["Description"].ToString(),
                        EmpID = int.Parse(row["EmpID"].ToString()),
                        Status = Boolean.Parse(row["Status"].ToString()),
                    };
                    users.Add(mapper.ToDTO(user));
                }
            }

            return users;
        }

        public ICollection<FunctionDTO> GetFunctions(int empID)
        {
            var functions = new List<FunctionDTO>();

            var users = GetUsers(empID);
            var userRepos = new UserRepository();
            var groups = new List<GroupDTO>();
            foreach (var user in users)
            {
                functions.AddRange(userRepos.GetFunctions(user.ID));
                groups.AddRange(userRepos.GetGroups(user.ID));
            }

            foreach (var group in groups)
            {
                functions.AddRange(group.ImplementFunctions);
            }

            var result = new List<FunctionDTO>();

            foreach (var func in functions)
            {
                if (result.Exists(f => f.ID == func.ID) == false)
                {
                    result.Add(func);
                }
            }
            result.Sort((x, y) => x.ID.CompareTo(y.ID));
            return result;
        }

    }
}