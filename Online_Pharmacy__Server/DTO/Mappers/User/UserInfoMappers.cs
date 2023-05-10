using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Models.User;
using OnlinePharmacy.Mappers.Generic;
using OnlinePharmacy.Repositories;
using System;

namespace OnlinePharmacy.DTO.Mappers.Generic
{
    public class UserInfoMapper : IMapper<IUser, UserInfoDTO>
    {
        private readonly UserInfoRepository userInfoRepos = new UserInfoRepository();
         
        public UserInfoDTO ToDTO(IUser obj)
        {
            string FirstName, LastName, Phone, Email, Address;
            GenderDTO Gender;
            DateTime? DOB;
            AccountDTO Account;

            if (obj is Customers cus)
            {
                FirstName = cus.FirstName;
                LastName = cus.LastName;
                Gender = userInfoRepos.GetGender(cus.GenderID);
                DOB = cus.DOB;
                Phone = cus.Phone;
                Email = cus.Email;
                Address = cus.Address;
                Account = new AccountDTO
                {
                    UserName = cus.UserName,
                    Password = cus.Password,
                    Status = cus.Status,
                };
            }
            else if (obj is Employees emp)
            {
                FirstName = emp.FirstName;
                LastName = emp.LastName;
                Gender = userInfoRepos.GetGender(emp.GenderID);
                DOB = emp.DOB;
                Phone = emp.Phone;
                Email = emp.Email;
                Address = emp.Address;
                Account = new AccountDTO
                {
                    UserName = emp.UserName,
                    Password = emp.Password,
                    Status = emp.Status,
                };
            }
            else
            {
                return null;
            }

            return new UserInfoDTO
            {
                FirstName = FirstName,
                LastName = LastName,
                FullName = FirstName + " " + LastName,
                Gender = Gender,
                DOB = DOB,
                Phone = Phone,
                Email = Email,
                Address = Address,
                Account = Account
            };
        }

        IUser IMapper<IUser, UserInfoDTO>.ToObject(UserInfoDTO dto)
        {
            throw new NotImplementedException();
        }
    
    }

}