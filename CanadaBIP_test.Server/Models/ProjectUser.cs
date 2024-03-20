using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CanadaBIP_test.Server.Models
{
    [Table("V_USERS", Schema = "dbo")]
    public class ProjectUser
    {
        public int ID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? RoleName { get; set; }
        public string? Sales_Area_Type { get; set; }
        public string? BU { get; set;}
        public string? BU_NAME { get; set;}
        public string? Sales_Area_Code { get; set; }
        public string? Sales_Area_Name { get; set; }
        public string? Parent_Email_Address { get; set; }
        public string? Parent_Sales_Area_Type { get; set; }
        public string? Parent_Sales_Area_Code { get; set; }
        public string? Parent_Sales_Area_Name { get; set; }

        public ProjectUser(int iD, string userName, string bU, string sales_Area_Code) 
        { 
            ID = iD;
            UserName = userName;
            BU = bU;
            Sales_Area_Code = sales_Area_Code;
        }
    }
}
