using System;
using System.Collections.Generic;
//
namespace OnlinePharmacy.DTO.Models.Product
{
    //
    public class ProductDTO
    {
        //
        public int ID { get; set; }
        //
        public string Name { get; set; }
        //
        public string Description { get; set; }
        //
        public bool Status { get; set; }
        //
        public int Score { get; set; }
        //
        //public int ProductType { get; set; }
        //
        public string Color { get; set; }
        //
        public string Size { get; set; }
        //
        public Nullable<double> Length { get; set; }
        //
        public Nullable<double> Width { get; set; }
        //
        public Nullable<double> Height { get; set; }
        //
        public Nullable<double> Weight { get; set; }
        //
        public int Quantity { get; set; }
        //
        public ICollection<FeedbackProductDTO> Feedbacks { get; set; }
        //
        public ICollection<ProductImageDTO> Images { get; set; }

    }

}