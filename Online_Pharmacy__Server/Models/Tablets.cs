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
    
    public partial class Tablets
    {
        public int ProductID { get; set; }
        public string ModelNumber { get; set; }
        public Nullable<double> MaxPressure { get; set; }
        public Nullable<double> MaxDiameter { get; set; }
        public Nullable<double> MaxDepth { get; set; }
        public Nullable<int> ProductionCapacity { get; set; }
    }
}
