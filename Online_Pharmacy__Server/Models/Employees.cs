//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Online_Pharmacy__Server.Models
{
    using System;
    using System.Collections.Generic;
    using OnlinePharmacy.DTO.Models.User;

    public partial class Employees : IUser
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GenderID { get; set; }
        public System.DateTime DOB { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int OrgID { get; set; }
        public Nullable<int> ReportTo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
    }
}
