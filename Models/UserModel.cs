using System.ComponentModel.DataAnnotations.Schema;

namespace CanadaBIP_test.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string? RoleName { get; set; }
        public string? BU { get; set; }
        public string? BU_NAME { get; set; }
        public string? Sales_Area_Type { get; set; }
        public string? Sales_Area_Code { get; set; }
        public string? Sales_Area_Name { get; set; }
        public string? Parent_Email_Address { get; set; }
        public string? Parent_Sales_Area_Type { get; set; }
        public string? Parent_Sales_Area_Code { get; set; }
        public string? Parent_Sales_Area_Name { get; set; }

        public User() { }

        public User(
            int id, 
            string userName, 
            string password,
            string roleName,
            string bu,
            string buName, 
            string salesAreaType,
            string salesAreaCode,
            string salesAreaName
        )
        {
            ID = id;
            UserName = userName;
            Password = password;
            RoleName = roleName;
            BU = bu;
            BU_NAME = buName;
            Sales_Area_Type = salesAreaType;
            Sales_Area_Code = salesAreaCode;
            Sales_Area_Name = salesAreaName;
        }
    }
}
