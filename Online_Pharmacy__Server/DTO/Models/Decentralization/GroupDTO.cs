using System.Collections.Generic;

//
namespace OnlinePharmacy.DTO.Models.Decentralization
{
    // final
    public class GroupDTO
    {
        public SubGroupDTO SimpleInformation { get; set; }
        //
        public string Description { get; set; }
        //
        public bool Status { get; set; }
        //
        public ICollection<FunctionDTO> ImplementFunctions { get; set; }

    }

    //
    public class SubGroupDTO
    {
        //
        public int ID { get; set; }
        //
        public string Name { get; set; }
    }

}