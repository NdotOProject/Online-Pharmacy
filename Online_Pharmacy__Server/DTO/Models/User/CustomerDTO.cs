//
using OnlinePharmacy.DTO.Models.Product;
using OnlinePharmacy.DTO.Models.Recruiment;
using System.Collections.Generic;

//
namespace OnlinePharmacy.DTO.Models.User
{
    public class CustomerDTO
    {
        // Customers.ID
        public int ID { get; set; }
        //
        public UserInfoDTO Information { get; set; }
        //
        public ICollection<CoverLetterDTO> CoverLetters { get; set; }
        //
        public ICollection<FeedbackProductDTO> Feedbacks { get; set; }

    }

}