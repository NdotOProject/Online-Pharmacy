using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Models.User;
using OnlinePharmacy.Mappers.Generic;

namespace OnlinePharmacy.DTO.Mappers.User
{
    //complete
    public class OrganizationMapper : IMapper<Organizations, OrganizationDTO>
    {
        public OrganizationDTO ToDTO(Organizations obj)
        {
            SubOrganizationDTO parent = null;
            if (obj.Parent != null)
            {
                Organizations org = AppConfig.DefaultDatabase()
                    .Organizations.Find(obj.Parent);
                if (org != null)
                {
                    parent = new SubOrganizationMapper().ToDTO(org);
                }
            }

            return new OrganizationDTO
            {
                ID = obj.ID,
                Name = obj.Name,
                Email= obj.Email,
                Phone = obj.Phone,
                Address = obj.Address,
                Description = obj.Description,
                Parent = parent,
            };
        }

        public Organizations ToObject(OrganizationDTO dto)
        {
            if (dto == null) { return null; }

            var obj = new Organizations
            {
                ID = dto.ID,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                Description = dto.Description,
            };
            if (dto.Parent != null)
            {
                obj.Parent = dto.Parent.ID;
            }
            
            return obj;
        }

    }

    // complete
    public class SubOrganizationMapper : IMapper<Organizations, SubOrganizationDTO>
    {
        public SubOrganizationDTO ToDTO(Organizations obj)
        {
            if (obj == null) { return null; }
            return new SubOrganizationDTO
            {
                ID = obj.ID,
                Name = obj.Name,
            };
        }

        public Organizations ToObject(SubOrganizationDTO dto)
        {
            if (dto == null) { return null; }
            return AppConfig.DefaultDatabase().Organizations.Find(dto.ID);
        }

    }

}