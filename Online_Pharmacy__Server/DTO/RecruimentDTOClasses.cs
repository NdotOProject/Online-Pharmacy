using OnlinePharmacy.DTO.External;
using OnlinePharmacy.DTO.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// 5 entites object
namespace OnlinePharmacy.DTO.Recruiment
{
    #region RecruimentNewsDTO
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
    #endregion

    #region CoverLetterDTO
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
    #endregion

    #region FeedbackRecruimentDTO
    public class FeedbackRecruimentDTO
    {
        public int ID { get; set; }
        public EmployeeDTO ReplyBy { get; set; }
        //public int LetterID { get; set; }
        public string Content { get; set; }
    }
    #endregion

    #region ResumeDTO
    public class ResumeDTO
    {
        // map to
        public int LetterID { get; set; }
        // map to
        public Nullable<double> YearOfExp { get; set; }
        // map to
        public string OldOrg { get; set; }
        // map to
        public string Website { get; set; }
        // map to
        public string Note { get; set; }

    }
    #endregion

    #region EducationDetailsDTO
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
    #endregion

}