//
using OnlinePharmacy.DTO.Models.User;
using System.Collections.Generic;

//
namespace OnlinePharmacy.DTO.Models.Decentralization
{
    // final
    public class UserDTO
    {
        //
        public SubUserDTO SimpleInformation { get; set; }
        //
        public SubEmployeeDTO Employee { get; set; }
        //
        public string Description { get; set; }
        //
        public bool Status { get; set; }
        //
        public ICollection<SubGroupDTO> BelongGroups { get; set; }
        //
        public ICollection<FunctionDTO> ImplementFunctions { get; set; }

    }

    //
    public class SubUserDTO
    {
        //
        public int ID { get; set; }
        //
        public string Name { get; set; }
    }

}