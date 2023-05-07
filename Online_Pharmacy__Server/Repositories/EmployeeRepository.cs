using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Decentralization;
using OnlinePharmacy.Mappers.Decentralization;
using OnlinePharmacy.Repositories.Decentralization;

namespace OnlinePharmacy.Repositories
{
    public class EmployeeRepository : BaseRepository
    {
        public ICollection<UserDTO> GetUsers(int EmpID)
        {
            var list = new List<UserDTO>();

            string sql = "select * from Users where EmpID=@EmpID";
            if (ExecuteQuery(sql,
                new SqlParameter[] { new SqlParameter("@EmpID", EmpID) },
                out DataRowCollection rows))
            {
                UserMapper mapper = new UserMapper();
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
                    list.Add(mapper.ToDTO(user));
                }
            }

            return list;
        }

        // get functions implement by employee 
        private ICollection<FunctionDTO> GetFunctions(int empID)
        {
            var functions = new List<FunctionDTO>();

            var users = new EmployeeRepository().GetUsers(empID);
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