using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Mappers.User;
using OnlinePharmacy.DTO.Models.Decentralization;
using OnlinePharmacy.DTO.Models.User;
using OnlinePharmacy.DTO.Mappers.Decentralization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlinePharmacy.Repositories.Decentralization
{
    // complete
    public class FunctionRepository
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();
        private readonly FunctionMapper funcMapper = new FunctionMapper();

        public ICollection<int> GetListId(ICollection<FunctionDTO> functions)
        {
            var list = new List<int>();

            foreach (var func in functions)
            {
                list.Add(func.ID);
            }

            return list;
        }

        public ICollection<FunctionDTO> GetSortedList(ICollection<FunctionDTO> functions)
        {
            var list = new List<FunctionDTO>(functions);
            list.Sort((x, y) => x.ID.CompareTo(y.ID));
            return list;
        }

        public ICollection<FunctionDTO> GetListDistinct(ICollection<FunctionDTO> functions)
        {
            return functions.Distinct().ToList();
        }

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