using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Pharmacy__Server.DTO
{
    /*
     Join các các dto của organizations, RoleDTO, employee để ra employeedto
     */

    [Serializable]
    public class EmployeeDTO
    {

    }

    public class EmployeeInfoDTO
    {
        public int Id { get; set; }
        // firstname + whitespace + lastname
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        // org_id => name
        public string MemberOf { get; set; }
        // report_to => name
        public string BossIs { get; set; }
        // user_id
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }


}