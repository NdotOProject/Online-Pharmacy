using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Pharmacy__Server.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Required]
        public bool Status { get; set; }
        //public ICollection<GroupDTO> BelongGroups { get; set; }
        public ICollection<FunctionDTO> HasFunctions { get; set; }
    }

    public class GroupDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Required]
        public bool Status { get; set; }
        public ICollection<FunctionDTO> HasFunctions { get; set; }
    }

    public class FunctionDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string ShortDescription { get; set; }
        [Required]
        [StringLength(255)]
        public string FullDescription { get; set; }
        //public string ApplyObject { get; set; }
        [Required]
        public bool Status { get; set; }
    }

}