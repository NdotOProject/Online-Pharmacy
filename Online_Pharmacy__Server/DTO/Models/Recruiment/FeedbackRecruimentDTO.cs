using OnlinePharmacy.DTO.Models.User;

//
namespace OnlinePharmacy.DTO.Models.Recruiment
{
    public class FeedbackRecruimentDTO
    {
        public int ID { get; set; }
        public EmployeeDTO ReplyBy { get; set; }
        //public int LetterID { get; set; }
        public string Content { get; set; }
    }
}