using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CanadaBIP_test.Server.Models
{
    // , Schema = "budget"
    [Table("USERS_12062022_OK", Schema = "dbo")]
    public class CurrentUser
    {
        public string? BU_NAME { get; set;}
        public string? Sales_Force_Code { get; set;}
        public string? Parent_Employee_Name { get; set;}
        public string? Parent_Email_Address { get; set;}
        public string? RoleName { get; set;}
        public string? Employee_Name { get; set;}
        [Key]
        public string Email_Address { get; set;}
        public string? Password { get; set;}
        public string? Sales_Area_Type { get; set;}
        public string? Sales_Force_Name { get; set;}
        public string? Sales_Area_Code { get; set;}
        public string? Sales_Area_Name { get; set;}
        public string? Parent_Sales_Area_Code { get; set;}
        public string? Parent_Sales_Area_Name { get; set;}
    }
}
