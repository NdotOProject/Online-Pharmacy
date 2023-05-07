using System;

namespace OnlinePharmacy.DTO.Generic
{
    #region UserInformationDTO
    public class UserInformationDTO
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
    #endregion

    #region AccountDTO
    public class AccountDTO
    {
        // Customers.UserName or Employees.UserName
        public string UserName { get; set; }
        // Customers.Password or Employees.Password
        public string Password { get; set; }
        // Customers.Status or Employees.Status
        public bool Status { get; set; }
    }
    #endregion

    #region GenderDTO
    public class GenderDTO
    {
        //
        public int ID { get; set; }
        //
        public string Name { get; set; }
    }
    #endregion

}