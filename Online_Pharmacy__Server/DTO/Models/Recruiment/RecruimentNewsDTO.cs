using OnlinePharmacy.DTO.Models.User;
using OnlinePharmacy.DTO.Models.Recruiment;
using System.Collections.Generic;

//
namespace OnlinePharmacy.DTO.Models.Recruiment
{
    //
    public class RecruimentNewsDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string Position { get; set; }
        public EmployeeDTO CreatedBy { get; set; }
        public bool Status { get; set; }
        public ICollection<CoverLetterDTO> CoverLetters { get; set; }

    }

}