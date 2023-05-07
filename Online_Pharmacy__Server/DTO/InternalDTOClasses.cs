using OnlinePharmacy.DTO.Decentralization;
using OnlinePharmacy.DTO.Generic;
using System.Collections.Generic;

//
namespace OnlinePharmacy.DTO.Internal
{
    #region OrganizationDTO
    public class OrganizationDTO
    {
        //
        public int ID { get; set; }
        //
        public string Name { get; set; }
        //
        public string Email { get; set; }
        //
        public string Phone { get; set; }
        //
        public string Address { get; set; }
        //
        public string Description { get; set; }
        //
        public SubOrganizationDTO Parent { get; set; }
    }

    public class SubOrganizationDTO
    {
        //
        public int ID { get; set; }
        //
        public string Name { get; set; }

    }
    #endregion

    #region EmployeeDTO
    public class EmployeeDTO
    {
        //
        public int ID { get; set; }
        //
        public UserInformationDTO Information { get; set; }
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
    #endregion

}