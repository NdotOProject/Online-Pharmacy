using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Mappers.Decentralization;
using OnlinePharmacy.DTO.Models.Decentralization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

namespace OnlinePharmacy.Repositories.Decentralization
{

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
                new SqlParameter("@GroupID", group.SimpleInformation.ID),
                new SqlParameter("@FunctionID", function.ID)
            };

            if (ExecuteUpdate(sql, paramaters, null, out object result))
            {
                var groups = db.Groups.Find(group.SimpleInformation.ID);
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
                new SqlParameter("@GroupID", group.SimpleInformation.ID),
                new SqlParameter("@FunctionID", function.ID)
            };

            if (ExecuteUpdate(sql, paramaters, null, out object result))
            {
                var groups = db.Groups.Find(group.SimpleInformation.ID);
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

        public bool IsSubGroup( GroupDTO group1, GroupDTO group2, out GroupDTO parent)
        {
            parent = null;

            var list1 = group1.ImplementFunctions;
            var list2 = group2.ImplementFunctions;

            var listFuncId = new List<int>();

            GroupDTO biggerGroup;
            if (list1.Count < list2.Count)
            {
                foreach (var func in list2)
                {
                    listFuncId.Add(func.ID);
                }
                biggerGroup = group2;                
            }
            else
            {
                foreach (var func in list1)
                {
                    listFuncId.Add(func.ID);
                }
                biggerGroup = group1;
            }

            if (ContainsFunctions(biggerGroup, listFuncId))
            {
                parent = biggerGroup;
                return true;
            }

            return false;
        }

        public bool ContainsFunctions(GroupDTO group, ICollection<int> listFuncId)
        {
            var listFuncs = group.ImplementFunctions;
            var listId = funcRepos.GetListId(listFuncs);

            ICollection<int> biggerList, smallerList;

            if (listFuncId.Count > listId.Count)
            {
                biggerList = listFuncId;
                smallerList = listId;
            }
            else
            {
                biggerList = listId;
                smallerList = listFuncId;
            }

            foreach (var id in smallerList)
            {
                if (biggerList.Contains(id) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public ICollection<GroupDTO> GetGroupsInList(ICollection<FunctionDTO> functions)
        {
            ICollection<GroupDTO> existGroups = GetGroups();
            ICollection<GroupDTO> list = new List<GroupDTO>();

            var listFuncId = funcRepos.GetListId(functions);

            foreach (var group in existGroups)
            {
                if (listFuncId.Count >= group.ImplementFunctions.Count
                    && ContainsFunctions(group, listFuncId))
                {
                    list.Add(group);
                }
            }

            ICollection<GroupDTO> groups = new List<GroupDTO>();
            for (int i = 0; i < list.Count - 1; i++)
            {
                GroupDTO group1 = list.ElementAt(i), group2 = list.ElementAt(i + 1);
                if (IsSubGroup(group1, group2,
                    out GroupDTO group))
                {
                    groups.Add(group);
                }
                else 
                {
                    groups.Add(group1);
                    groups.Add(group2);
                }
            }

            return groups;
        }
    }

}