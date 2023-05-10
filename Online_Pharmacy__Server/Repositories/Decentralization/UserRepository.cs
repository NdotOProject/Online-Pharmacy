using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Mappers.Decentralization;
using OnlinePharmacy.DTO.Mappers.User;
using OnlinePharmacy.DTO.Models.Decentralization;
using OnlinePharmacy.DTO.Models.User;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Antlr.Runtime.Misc;
using System.Web.UI.WebControls;

namespace OnlinePharmacy.Repositories.Decentralization
{

    public class UserRepository : BaseRepository
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();
        private readonly FunctionRepository funcRepos = new FunctionRepository();
        private readonly UserMapper userMapper = new UserMapper();



        public bool UpdateUser(UserDTO userDTO)
        {
            Users users = userMapper.ToObject(userDTO);
            db.Entry(users).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                var dtoGroups = userDTO.BelongGroups.ToList();
                var dtoFuncs = userDTO.ImplementFunctions.ToList();
                userDTO = userMapper.ToDTO(users);
                var funcs = userDTO.ImplementFunctions.ToList();

                if (funcs.Count == 0)
                {
                    foreach (var func in dtoFuncs)
                    {
                        userDTO = AddFunction(func, userDTO);
                    }
                    return true;
                }

                userDTO = RemoveAllGroups(userDTO);

                foreach (var group in dtoGroups)
                {
                    userDTO = AddGroup(group, userDTO);
                }

                userDTO = RemoveAllFunctions(userDTO);

                foreach (var func in dtoFuncs)
                {
                    userDTO = AddFunction(func, userDTO);
                }

                return true;
            }

            return false;
        }

        private UserDTO RemoveAllFunctions(UserDTO userDTO)
        {
            var funcs = userDTO.ImplementFunctions.ToList();

            foreach (var func in funcs)
            {
                userDTO = RemoveFunction(func, userDTO);
            }

            return userDTO;
        }

        public UserDTO GetUser(int userId)
        {
            var users = db.Users.Find(userId);
            return userMapper.ToDTO(users);
        }

        private UserDTO RemoveAllGroups(UserDTO user)
        {
            var groups = user.BelongGroups.ToList();

            foreach (var group in groups)
            {
                user = RemoveGroup(group, user);
            }

            return user;
        }

        private UserDTO AddGroup(SubGroupDTO group, UserDTO user)
        {
            string sql = "insert into UserGroup (UserID, GroupID)" +
                        " values (@UserID, @GroupID)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", user.SimpleInformation.ID),
                new SqlParameter("@GroupID", group.ID),
            };

            if (ExecuteUpdate(sql, parameters, null, out object result))
            {
                return GetUser(user.SimpleInformation.ID);
            }

            return null;
        }

        public UserDTO AddGroup(GroupDTO group, UserDTO user)
        {
            /*
            string sql = "insert into UserGroup (UserID, GroupID)" +
                        " values (@UserID, @GroupID)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", user.SimpleInformation.ID),
                new SqlParameter("@GroupID", group.SimpleInformation.ID),
            };

            if (ExecuteUpdate(sql, parameters, null, out object result))
            {
                return GetUser(user.SimpleInformation.ID);
            }
            */
            return AddGroup(group.SimpleInformation, user);
        }

        public UserDTO RemoveGroup(SubGroupDTO group, UserDTO user)
        {
            string sql = "delete from UserGroup" +
                        " where UserID=@UserID" +
                        " and GroupID=@GroupID";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", user.SimpleInformation.ID),
                new SqlParameter("@GroupID", group.ID),    
            };

            if (ExecuteUpdate(sql, parameters, null, out object result))
            {
                return GetUser(user.SimpleInformation.ID);
            }
            return null;
        }

        public ICollection<GroupDTO> GetGroups(int UserID)
        {
            string sql = "select GroupID from UserGroup where UserID=@UserID";
            var list = new List<GroupDTO>();

            if (ExecuteQuery(sql,
                new SqlParameter[] { new SqlParameter("@UserID", UserID) },
                out DataRowCollection rows))
            {
                GroupMapper mapper = new GroupMapper();
                foreach (DataRow row in rows)
                {
                    if (int.TryParse(row["GroupID"].ToString(), out int GroupID))
                    {
                        var group = AppConfig.DefaultDatabase().Groups.Where(x => x.ID == GroupID).First();
                        list.Add(mapper.ToDTO(group));
                    }
                }
            }

            return list;
        }

        public ICollection<SubGroupDTO> GetSubGroups(int UserID)
        {
            var subGroups = new List<SubGroupDTO>();

            foreach (var group in GetGroups(UserID))
            {
                subGroups.Add(group.SimpleInformation);
            }

            return subGroups;
        }

        public ICollection<FunctionDTO> GetFunctions(int UserID)
        {
            string sql = "select FunctionID" +
                        " from UserFunction" +
                        " where UserID=@UserID";
            var functions = new List<FunctionDTO>();
            SqlParameter[] parameters = { new SqlParameter("@UserID", UserID) };

            if (ExecuteQuery(sql, parameters, out DataRowCollection rows))
            {
                foreach (DataRow row in rows)
                {
                    if (int.TryParse(row["FunctionID"].ToString(), out int FunctionID))
                    {
                        functions.Add(funcRepos.GetFunction(FunctionID));
                    }
                }
            }

            /*
            var groups = GetGroups(UserID);

            foreach (var group in groups)
            {
                functions.AddRange(group.ImplementFunctions);
            }

            functions = (List<FunctionDTO>)funcRepos.GetListDistinct(functions);
            */
            return funcRepos.GetSortedList(functions);
        }

        public SubEmployeeDTO GetEmployee(int UserID)
        {
            var user = db.Users.Find(UserID);
            if (user != null)
            {
                var emp = db.Employees.Find(user.EmpID);
                return new SubEmployeeMapper().ToDTO(emp);
            }

            return null;
        }

        public UserDTO AddFunction(FunctionDTO function, UserDTO user)
        {
            string sql = "insert into UserFunction (UserID, FunctionID)" +
                        " values (@UserID, @FunctionID)";
            SqlParameter[] paramaters =
            {
                new SqlParameter("@UserID", user.SimpleInformation.ID),
                new SqlParameter("@UserID", function.ID)
            };

            if (ExecuteUpdate(sql, paramaters, null, out object result))
            {
                var users = db.Users.Find(user.SimpleInformation.ID);
                return userMapper.ToDTO(users);
            }

            return null;
        }

        public UserDTO RemoveFunction(FunctionDTO function, UserDTO user)
        {
            string sql = "delete from UserFunction" +
                        " where UserID=@UserID" +
                        " and FunctionID=@FunctionID";
            SqlParameter[] paramaters =
            {
                new SqlParameter("@UserID", user.SimpleInformation.ID),
                new SqlParameter("@FunctionID", function.ID)
            };

            if (ExecuteUpdate(sql, paramaters, null, out object result))
            {
                return GetUser(user.SimpleInformation.ID);
            }

            return null;
        }


    }

}