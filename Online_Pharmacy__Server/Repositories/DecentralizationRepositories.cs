using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Decentralization;
using OnlinePharmacy.Mappers.Decentralization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlinePharmacy.Repositories.Decentralization
{
    
    public class UserRepository : BaseRepository
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();
        private readonly FunctionRepository functionRepository = new FunctionRepository();
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

        public ICollection<FunctionDTO> GetFunctions(int UserID)
        {
            string sql = "select FunctionID from UserFunction where UserID=@UserID";
            var functions = new List<FunctionDTO>();

            if (ExecuteQuery(sql,
                new SqlParameter[] { new SqlParameter("@UserID", UserID) },
                out DataRowCollection rows))
            {
                foreach (DataRow row in rows)
                {
                    if (int.TryParse(row["FunctionID"].ToString(), out int FunctionID))
                    {
                        functions.Add(functionRepository.GetFunction(FunctionID));
                    }
                }
            }

            var groups = GetGroups(UserID);

            foreach (var group in groups)
            {
                functions.AddRange(group.ImplementFunctions);
            }

            var list = new List<FunctionDTO>();

            foreach (FunctionDTO func in functions)
            {
                if (list.Contains(func) == false)
                {
                    list.Add(func);
                }
            }

            functions = list;

            list.Clear();
            list = null;

            functions.Sort((x, y) => x.ID.CompareTo(y.ID));

            return functions;
        }

        

    }

    public class GroupRepository : BaseRepository
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();
        private readonly GroupMapper groupMapper = new GroupMapper();
        private readonly FunctionRepository funcRepos = new FunctionRepository();

        public GroupDTO AddFunction(FunctionDTO function, GroupDTO group)
        {
            string sql = "insert into GroupFunction (GroupID, FunctionID)" +
                        " values (@GroupID, @FunctionID)";
            SqlParameter[] paramaters =
            {
                new SqlParameter("@GroupID", group.ID),
                new SqlParameter("@FunctionID", function.ID)
            };

            if (ExecuteUpdate(sql, paramaters, null, out object result))
            {
                var groups = db.Groups.Find(group.ID);
                return groupMapper.ToDTO(groups);
            }
            return null;
        }

        public GroupDTO RemoveFunction(FunctionDTO function, GroupDTO group)
        {
            string sql = "delete from GroupFunction" +
                        " where GroupID=@GroupID" +
                        " and FunctionID=@FunctionID";
            SqlParameter[] paramaters =
            {
                new SqlParameter("@GroupID", group.ID),
                new SqlParameter("@FunctionID", function.ID)
            };

            if (ExecuteUpdate(sql, paramaters, null, out object result))
            {
                var groups = db.Groups.Find(group.ID);
                return groupMapper.ToDTO(groups);
            }
            return null;
        }

        public ICollection<FunctionDTO> GetFunctions(int GroupID)
        {
            string sql = "select FunctionID" +
                        " from GroupFunction" +
                        " where GroupID=@GroupID" +
                        " order by FunctionID asc";
            SqlParameter[] paramaters =
            {
                new SqlParameter("@GroupID", GroupID)
            };

            var functions = new List<FunctionDTO>();

            if (ExecuteQuery(sql, paramaters, out DataRowCollection rows))
            {
                foreach (DataRow row in rows)
                {
                    if (int.TryParse(row["FunctionID"].ToString(), out int FunctionID))
                    {
                        var func = funcRepos.GetFunction(FunctionID);
                        if (func.Status)
                        {
                            functions.Add(func);
                        }
                    }
                }
            }

            return functions;
        }

        // custom update, create, delete
        public GroupDTO CreateGroup(GroupDTO groupDTO)
        {
            Groups groups = groupMapper.ToObject(groupDTO);
            
            db.Groups.Add(groups);
            if (db.SaveChanges() > 0)
            {
                groups = db.Groups.Find(groups.ID);
                var groupFuncs = groupDTO.ImplementFunctions;
                groupDTO = groupMapper.ToDTO(groups);
                if (groupFuncs != null)
                {
                    foreach (FunctionDTO func in groupFuncs)
                    {
                        groupDTO = AddFunction(func, groupDTO);
                    }
                    return groupDTO;
                }
            }
            return null;
        }

        public bool UpdateGroup(GroupDTO groupDTO)
        {
            Groups groups = groupMapper.ToObject(groupDTO);

            db.Entry(groups).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                var dtofuncs = groupDTO.ImplementFunctions;
                groupDTO = groupMapper.ToDTO(groups);
                var funcs = groupDTO.ImplementFunctions;

                if (funcs.Count == 0)
                {
                    foreach (var func in dtofuncs)
                    {
                        groupDTO = AddFunction(func, groupDTO);
                    }
                    return true;
                }

                for (int i = 0; i < funcs.Count; i++)
                {
                    var func = funcs.ElementAt(i);
                    if (i < dtofuncs.Count)
                    {
                        var dtofunc = dtofuncs.ElementAt(i);
                        if (func != dtofunc)
                        {
                            groupDTO = RemoveFunction(func, groupDTO);
                            if (groupDTO == null)
                            {
                                return false;        
                            }
                            groupDTO = AddFunction(dtofunc, groupDTO);
                            if (groupDTO == null)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        groupDTO = RemoveFunction(func, groupDTO);
                    }
                }

                return true;
            }

            return false;
        }

        public GroupDTO DeleteGroup(int groupID)
        {
            Groups groups = db.Groups.Find(groupID);
            if (groups != null)
            {
                groups.Status = false;
                if (db.SaveChanges() > 0)
                {
                    return groupMapper.ToDTO(groups);
                }
            }

            return null;
        }

    }

    // complete
    public class FunctionRepository
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();
        private readonly FunctionMapper funcMapper = new FunctionMapper();

        // read
        public ICollection<FunctionDTO> GetFunctions()
        {
            var funcs = db.Functions;
            var list = new List<FunctionDTO>();

            foreach (var function in funcs)
            {
                list.Add(funcMapper.ToDTO(function));
            }

            return list;
        }

        public FunctionDTO GetFunction(int FuncID)
        {
            Functions function = db.Functions.Find(FuncID);
            return funcMapper.ToDTO(function);
        }

        // create
        public int CreateFuncion(FunctionDTO function)
        {
            Functions func = funcMapper.ToObject(function);
            db.Functions.Add(func);
            if (db.SaveChanges() > 0)
            {
                return func.ID;
            }
            return 0;
        }

        // update
        public bool UpdateFunction(FunctionDTO function)
        {
            Functions func = funcMapper.ToObject(function);
            db.Entry(func).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        // delete
        public FunctionDTO DeleteFunction(int FuncID)
        {
            Functions function = db.Functions.Find(FuncID);
            if (function != null)
            {
                function.Status = false;
                if (db.SaveChanges() > 0)
                {
                    return funcMapper.ToDTO(function);
                }
            }
            return null;
        }

    }

}