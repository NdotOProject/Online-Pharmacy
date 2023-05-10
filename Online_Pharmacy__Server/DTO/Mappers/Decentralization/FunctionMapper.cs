using OnlinePharmacy.DTO.Models.Decentralization;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.Mappers.Generic;

//
namespace OnlinePharmacy.DTO.Mappers.Decentralization
{
    // complete
    public class FunctionMapper : IMapper<Functions, FunctionDTO>
    {
        public FunctionDTO ToDTO(Functions obj)
        {
            if (obj == null) { return null; }
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
            if (dto == null) { return null; }
            return new Functions
            {
                ID = dto.ID,
                ShortDescription = dto.ShortDescription,
                FullDescription = dto.FullDescription,
                Status = dto.Status
            };
        }
    }

}