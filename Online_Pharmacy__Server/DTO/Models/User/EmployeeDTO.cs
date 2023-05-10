using OnlinePharmacy.DTO.Models.Decentralization;
using System.Collections.Generic;

//
namespace OnlinePharmacy.DTO.Models.User
{
    //
    public class EmployeeDTO
    {
        //
        public int ID { get; set; }
        //
        public UserInfoDTO Information { get; set; }
        //
        public SubOrganizationDTO Organization { get; set; }
        //
        public SubEmployeeDTO Boss { get; set; }
        //
        public ICollection<SubUserDTO> Users { get; set; }
        //
        public ICollection<FunctionDTO> ImplementFunctions { get; set; }

    }

    public class SubEmployeeDTO
    {
        //
        public int ID { get; set; }
        //
        public string FullName { get; set; }

    }

}