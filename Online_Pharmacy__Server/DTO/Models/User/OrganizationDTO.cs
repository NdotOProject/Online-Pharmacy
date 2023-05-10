//
namespace OnlinePharmacy.DTO.Models.User
{
    //
    public class OrganizationDTO
    {
        //
        public int ID { get; set; }
        //
        public string Name { get; set; }
        //
        public string Email { get; set; }
        //
        public string Phone { get; set; }
        //
        public string Address { get; set; }
        //
        public string Description { get; set; }
        //
        public SubOrganizationDTO Parent { get; set; }
    }

    public class SubOrganizationDTO
    {
        //
        public int ID { get; set; }
        //
        public string Name { get; set; }

    }
}