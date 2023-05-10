using OnlinePharmacy.DTO.Models.User;
using System.Collections.Generic;

//
namespace OnlinePharmacy.DTO.Models.Recruiment
{
    //
    public class CoverLetterDTO
    {
        // CoverLetters.ID
        public int ID { get; set; }
        // CoverLetters.NewsID
        //public RecruimentNewsDTO News { get; set; }
        // CoverLetters.ApplyBy
        public CustomerDTO Candidate { get; set; }
        // CoverLetters.Position
        public string Position { get; set; }
        // CoverLetters.Content
        public string Content { get; set; }
        // CoverLetters.Note
        public string Note { get; set; }
        //
        public ResumeDTO Resume { get; set; }
        //
        public EducationDetailsDTO EducationDetails { get; set; }
        //
        public ICollection<FeedbackRecruimentDTO> Feedbacks { get; set; }

    }

}