//
namespace OnlinePharmacy.DTO.Models.Recruiment
{
    public class EducationDetailsDTO
    {
        // map to EducationDetails.LetterID
        public int LetterID { get; set; }
        // map to EducationDetails.CollegeName
        public string CollegeName { get; set; }
        // map to EducationDetails.CollegeAddress
        public string CollegeAddress { get; set; }
        // map to EducationDetails.Degree
        public string Degree { get; set; }
        // map to EducationDetails.Certificate
        public string Certificate { get; set; }
        // map to EducationDetails.Status
        public bool Status { get; set; }

    }

}