using OnlinePharmacy.DTO.Models.User;
using System;
//
namespace OnlinePharmacy.DTO.Models.Product
{
    public class FeedbackProductDTO
    {
        //
        public int ID { get; set; }
        //
        public Nullable<int> Rate { get; set; }
        //
        public string Content { get; set; }
        //
        public CustomerDTO SendBy { get; set; }
        //public int ProductID { get; set; }
        //
        public SubFeedbackProductDTO ReplyTo { get; set; }
        //
        public EmployeeDTO ReplyBy { get; set; }
        //
        public bool Status { get; set; }
    }

    public class SubFeedbackProductDTO
    {
        public int ID { get; set; }
        //
        public Nullable<int> Rate { get; set; }
        //
        public string Content { get; set; }
        //
        public CustomerDTO SendBy { get; set; }
        //
        public bool Status { get; set; }
    }

}