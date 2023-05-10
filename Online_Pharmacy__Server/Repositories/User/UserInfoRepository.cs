using Online_Pharmacy__Server.App_Start;
using Online_Pharmacy__Server.Models;
using OnlinePharmacy.DTO.Models.User;
using OnlinePharmacy.Mappers.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlinePharmacy.Repositories
{
    public class UserInfoRepository
    {
        private readonly OnlinePharmacyEntities db = AppConfig.DefaultDatabase();

        public GenderDTO GetGender(int? id)
        {
            Gender gender = db.Gender.Find(id);
            return new GenderMapper().ToDTO(gender);
        }



    }
}