using OnlinePharmacy.DTO.Generic;
using OnlinePharmacy.DTO.Product;
using OnlinePharmacy.DTO.Recruiment;
using System.Collections.Generic;

//
namespace OnlinePharmacy.DTO.External
{
    #region CustomerDTO
    public class CustomerDTO
    {
        // Customers.ID
        public int ID { get; set; }
        //
        public UserInformationDTO Information { get; set; }
        //
        public ICollection<CoverLetterDTO> CoverLetters { get; set; }
        //
        public ICollection<FeedbackProductDTO> Feedbacks { get; set; }

    }
    #endregion

}