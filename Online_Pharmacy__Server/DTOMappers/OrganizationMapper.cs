using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Internal;
using OnlinePharmacy.Mappers.Generic;
using System.Linq;

namespace OnlinePharmacy.Mappers
{
    //complete
    public class OrganizationMapper : IMapper<Organizations, OrganizationDTO>
    {
        public OrganizationDTO ToDTO(Organizations obj)
        {
            Organizations org = null;
            if (obj.Parent != null)
            {
                org = AppConfig.DefaultDatabase()
                    .Organizations.Where(o => o.ID == obj.Parent)
                    .ToList()[0];
            }

            SubOrganizationDTO parent = null;
            if (org != null)
            {
                parent = new SubOrganizationMapper().ToDTO(org);
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
            return AppConfig.DefaultDatabase()
                .Organizations
                .Where(o => o.ID == dto.ID)
                .ToList()[0];
        }

    }

}