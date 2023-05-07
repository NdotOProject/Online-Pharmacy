using System.Collections.Generic;

//
namespace OnlinePharmacy.DTO.Decentralization
{

    #region UserDTO
    public class UserDTO
    {
        //
        public int ID { get; set; }
        //
        public string Name { get; set; }
        //
        public string Description { get; set; }
        //public SubEmployeeDTO Employee { get; set; }
        //
        public bool Status { get; set; }
        //
        public ICollection<SubGroupDTO> BelongGroups { get; set; }
        //
        public ICollection<FunctionDTO> ImplementFunctions { get; set; }

    }

    public class SubUserDTO
    {
        //
        public int ID { get; set; }
        //
        public string Name { get; set; }
    }
    #endregion

    #region GroupDTO
    public class GroupDTO
    {
        //
        public int ID { get; set; }
        //
        public string Name { get; set; }
        //
        public string Description { get; set; }
        //
        public bool Status { get; set; }
        //
        public ICollection<FunctionDTO> ImplementFunctions { get; set; }

    }

    public class SubGroupDTO
    {
        //
        public int ID { get; set; }
        //
        public string Name { get; set; }
    }
    #endregion

    #region FunctionDTO
    // Map FunctionDTO to with Functions (1:1)
    public class FunctionDTO
    {
        //
        public int ID { get; set; }
        //
        public string ShortDescription { get; set; }
        //
        public string FullDescription { get; set; }
        //
        public bool Status { get; set; }

    }
    #endregion

}