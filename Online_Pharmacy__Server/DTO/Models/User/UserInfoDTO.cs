using System;

//
namespace OnlinePharmacy.DTO.Models.User
{
    //
    public class UserInfoDTO
    {
        // Customers.FirstName or Employees.FirstName
        public string FirstName { get; set; }
        // Customers.LastName or Employees.LastName
        public string LastName { get; set; }
        // FirstName + " " + LastName
        public string FullName { get; set; }
        // Customers.GenderID or Employees.GenderID
        public GenderDTO Gender { get; set; }
        // Customers.DOB or Employees.DOB
        public Nullable<System.DateTime> DOB { get; set; }
        // Customers.Phone or Employees.Phone
        public string Phone { get; set; }
        // Customers.Email or Employees.Email
        public string Email { get; set; }
        // Customers.Address or Employees.Address
        public string Address { get; set; }
        //
        public AccountDTO Account { get; set; }

    }

}