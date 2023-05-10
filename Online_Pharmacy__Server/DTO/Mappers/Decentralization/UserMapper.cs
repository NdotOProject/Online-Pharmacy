//
using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Models.Decentralization;
using OnlinePharmacy.Mappers.Generic;
using OnlinePharmacy.Repositories.Decentralization;

//
namespace OnlinePharmacy.DTO.Mappers.Decentralization
{
    // complete
    public class UserMapper : IMapper<Users, UserDTO>
    {
        private readonly UserRepository userRepos = new UserRepository();

        public UserDTO ToDTO(Users obj)
        {
            if (obj == null) { return null; }
            return new UserDTO
            {
                SimpleInformation = new SubUserDTO
                {
                    ID = obj.ID,
                    Name = obj.Name,
                },
                Employee = userRepos.GetEmployee(obj.ID),
                Description = obj.Description,
                Status = obj.Status,
                BelongGroups = userRepos.GetSubGroups(obj.ID),
                ImplementFunctions = userRepos.GetFunctions(obj.ID),
            };
        }

        public Users ToObject(UserDTO dto)
        {
            if (dto == null) { return null; }

            return new Users
            {
                ID = dto.SimpleInformation.ID,
                Name = dto.SimpleInformation.Name,
                Description = dto.Description,
                Status = dto.Status,
                //EmpID = dto.Employee.ID,
            };
        }

    }

    public class SubUserMapper : IMapper<Users, SubUserDTO>
    {
        public SubUserDTO ToDTO(Users obj)
        {
            if (obj == null) { return null; }
            return new SubUserDTO
            {
                ID = obj.ID,
                Name = obj.Name
            };
        }

        public Users ToObject(SubUserDTO dto)
        {
            if (dto == null) { return null; }
            return AppConfig.DefaultDatabase().Users.Find(dto.ID);
        }

    }

}