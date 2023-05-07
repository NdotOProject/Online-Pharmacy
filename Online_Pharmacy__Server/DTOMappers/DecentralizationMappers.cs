using Online_Pharmacy__Server.App_Start;
using OnlinePharmacy.DTO.Decentralization;
using Online_Pharmacy__Server.Models;
using System.Collections.Generic;
using System.Linq;
using OnlinePharmacy.Mappers.Generic;
using OnlinePharmacy.Repositories.Decentralization;
using OnlinePharmacy.DTO.Mappers;

namespace OnlinePharmacy.Mappers.Decentralization
{
    public class UserMapper : IMapper<Users, UserDTO>
    {
        private readonly UserRepository userRepos =  new UserRepository();

        public UserDTO ToDTO(Users obj)
        {
            return new UserDTO
            {
                ID = obj.ID,
                Name = obj.Name,
                Description = obj.Description,
                Status = obj.Status,
                ImplementFunctions = userRepos.GetFunctions(obj.ID),
            };
        }

        public Users ToObject(UserDTO dto)
        {
            return new Users
            {
                ID = dto.ID,
                Name = dto.Name,
                Description = dto.Description,
                Status = dto.Status,
                //user.EmpID = userRepos.GetEmployee().ID
            };
        }

    }

    #region GroupMapper: complete
    public class GroupMapper : IMapper<Groups, GroupDTO>
    {
        // map generic fields and add functions
        public GroupDTO ToDTO(Groups obj)
        {
            return new GroupDTO
            {
                ID = obj.ID,
                Name = obj.Name,
                Description = obj.Description,
                Status = obj.Status,
                ImplementFunctions = new GroupRepository().GetFunctions(obj.ID)
            };
        }

        // map 1:1
        public Groups ToObject(GroupDTO dto)
        {
            return new Groups
            {
                ID = dto.ID,
                Name = dto.Name,
                Description = dto.Description,
                Status = dto.Status
            };
        }
    }
    #endregion

    #region FunctionMapper: complete
    // map 1:1
    public class FunctionMapper : IMapper<Functions, FunctionDTO>
    {
        public FunctionDTO ToDTO(Functions obj)
        {
            return new FunctionDTO
            {
                ID = obj.ID,
                ShortDescription = obj.ShortDescription,
                FullDescription = obj.FullDescription,
                Status = obj.Status
            };
        }

        public Functions ToObject(FunctionDTO dto)
        {
            return new Functions
            {
                ID = dto.ID,
                ShortDescription = dto.ShortDescription,
                FullDescription = dto.FullDescription,
                Status = dto.Status
            };
        }
    }
    #endregion

    

}