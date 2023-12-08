using System.ComponentModel.DataAnnotations.Schema;

namespace CanadaBIP_test.Models
{
    [Table("V_Budget_Manager_Representative", Schema = "budget")]
    public class BMRepresentativeViewModel
    {
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? BU { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? BU_NAME { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Manager_Sales_Force_Code { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Manager_Sales_Force_Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Manager_Sales_Area_Code { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Manager_Sales_Area_Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Manager_Sales_Area_Type { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Manager_Employee_ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Manager_Employee_Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Manager_Email_Address { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Rep_Sales_Force_Code { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Rep_Sales_Force_Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Rep_Sales_Area_Code { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Rep_Sales_Area_Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Rep_Sales_Area_Type { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Rep_Employee_ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Rep_Employee_Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Rep_Email_Address { get; set; }
        public DateTime? Date_Entry { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Product { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? Amount_Budget { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal Amount_Allocated { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? Amount_Left { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Specific_Initiative { get; set; }
        public int? Status { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Creator { get; set; }
        public DateTime? Created { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Changer { get; set; }
        public DateTime? Changed { get; set; }
        public int? Is_BR { get; set; }
        public int? Is_Expired { get; set; }
    }

    public class BMRepresentativeEditModel
    {
        public int ID { get; set; }
        public string? Int_Usr_ID { get; set; }
        public string? Step { get; set; }
        public string? BU { get; set; }
        public string? Manager_Sales_Area_Code { get; set; }
        public string? Sales_Area_Code { get; set; }
        public DateTime? Date_Entry { get; set; }
        public string? Product { get; set; }

        [Column(TypeName = "decimal(25, 2)")]
        public decimal? Amount_Allocated { get; set; }
    }

    [Table("V_Budget_BMR_Combobox_Product", Schema = "budget")]
    public class BMRepProductModel
    {
        public int ID { get; set; }
        public string Sales_Area_Code { get; set; }
        public string Product { get; set; }
    }

    [Table("V_Budget_BMR_Combobox_RepNames", Schema = "budget")]
    public class BMRepNameModel
    {
        public int ID { get; set; }
        public string? Sales_Area_Code { get; set; }
        public string? Sales_Area_Name { get; set; }
        public string? Parent_Sales_Area_Code { get; set; }
        public string? Employee_Name { get; set; }
        public string? Employee_ID { get; set; }
        public string? Email_Address { get; set; }
        public string Product { get; set; }
    }
}
