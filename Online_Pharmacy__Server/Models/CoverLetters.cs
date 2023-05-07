//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Online_Pharmacy__Server.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CoverLetters
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CoverLetters()
        {
            this.FeedbackRecruiment = new HashSet<FeedbackRecruiment>();
        }
    
        public int ID { get; set; }
        public int NewsID { get; set; }
        public int ApplyBy { get; set; }
        public string Position { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
    
        public virtual Customers Customers { get; set; }
        public virtual RecruimentNews RecruimentNews { get; set; }
        public virtual EducationDetails EducationDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeedbackRecruiment> FeedbackRecruiment { get; set; }
        public virtual Resume Resume { get; set; }
    }
}
