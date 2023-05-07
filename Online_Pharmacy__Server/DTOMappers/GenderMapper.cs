using OnlinePharmacy.DTO.Generic;
using Online_Pharmacy__Server.Models;

namespace OnlinePharmacy.Mappers.Generic
{
    // complete
    // Map to with Gender (1:1)
    public class GenderMapper : IMapper<Gender, GenderDTO>
    {
        public GenderDTO ToDTO(Gender obj)
        {
            return new GenderDTO
            {
                ID = obj.ID,
                Name = obj.Name
            };
        }

        public Gender ToObject(GenderDTO dto)
        {
            return new Gender
            {
                ID = dto.ID,
                Name = dto.Name
            };
        }
    }
}