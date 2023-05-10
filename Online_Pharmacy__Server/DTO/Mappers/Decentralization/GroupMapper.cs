using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Models.Decentralization;
using OnlinePharmacy.Mappers.Generic;
using OnlinePharmacy.Repositories.Decentralization;

//
namespace OnlinePharmacy.DTO.Mappers.Decentralization
{
    // complete
    public class GroupMapper : IMapper<Groups, GroupDTO>
    {
        public GroupDTO ToDTO(Groups obj)
        {
            if (obj == null) { return null; }
            return new GroupDTO
            {
                SimpleInformation = new SubGroupDTO
                {
                    ID = obj.ID,
                    Name = obj.Name
                },
                Description = obj.Description,
                Status = obj.Status,
                ImplementFunctions = new GroupRepository().GetFunctions(obj.ID)
            };
        }

        public Groups ToObject(GroupDTO dto)
        {
            if (dto == null) { return null; }
            return new Groups
            {
                ID = dto.SimpleInformation.ID,
                Name = dto.SimpleInformation.Name,
                Description = dto.Description,
                Status = dto.Status
            };
        }

    }

    public class SubGroupMapper : IMapper<Groups, SubGroupDTO>
    {
        public SubGroupDTO ToDTO(Groups obj)
        {
            if (obj == null) { return null; }
            return new SubGroupDTO
            {
                ID = obj.ID,
                Name = obj.Name
            };
        }

        public Groups ToObject(SubGroupDTO dto)
        {
            if (dto == null) { return null; }
            return AppConfig.DefaultDatabase().Groups.Find(dto.ID);
        }

    }

}