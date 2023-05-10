//
namespace OnlinePharmacy.DTO.Models.User
{
    //
    public class AccountDTO
    {
        // Customers.UserName or Employees.UserName
        public string UserName { get; set; }
        // Customers.Password or Employees.Password
        public string Password { get; set; }
        // Customers.Status or Employees.Status
        public bool Status { get; set; }

    }
}