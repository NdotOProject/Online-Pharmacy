using OnlinePharmacy.DTO.External;
using OnlinePharmacy.DTO.Internal;
using System;
using System.Collections.Generic;
//
namespace OnlinePharmacy.DTO.Product
{
    #region ProductTypeDTO
    public class ProductTypeDTO
    {
        //
        public int ID { get; set; }
        //
        public string Name { get; set; }
        //
        public bool Status { get; set; }
    }
    #endregion

    #region ProductImageDTO
    public class ProductImageDTO
    {
        //
        public int ID { get; set; }
        //public int ProductID { get; set; }
        //
        public byte[] Picture { get; set; }

    }
    #endregion

    #region FeedbackProductDTO
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
    #endregion

    #region EncapsulationDTO
    public class EncapsulationDTO
    {
        //public int ProductID { get; set; }
        public string OutPut { get; set; }
        public string CapsuleSize { get; set; }

    }
    #endregion

    #region LiquidFillingDTO
    public class LiquidFillingDTO
    {
        //public int ProductID { get; set; }
        //
        public Nullable<double> AirPressure { get; set; }
        //
        public Nullable<double> AirVolume { get; set; }
        //
        public Nullable<int> FillingSpeed { get; set; }
        //
        public Nullable<int> FillingRange { get; set; }

    }
    #endregion

    #region TabletDTO
    public class TabletDTO
    {
        //public int ProductID { get; set; }
        //
        public string ModelNumber { get; set; }
        //
        public Nullable<double> MaxPressure { get; set; }
        //
        public Nullable<double> MaxDiameter { get; set; }
        //
        public Nullable<double> MaxDepth { get; set; }
        //
        public Nullable<int> ProductionCapacity { get; set; }

    }
    #endregion

    #region ProductDTO
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
    #endregion

}