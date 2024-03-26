using System.ComponentModel.DataAnnotations.Schema;

namespace CanadaBIP_test.Server.Models
{
    [Table("V_Budget_Representative", Schema = "budget")]
    public class BudgetRepresentativeModel
    {
        public int ID { get; set; }
        public string? BU { get; set; }
        public string? BU_NAME { get; set; }
        public string? Manager_Sales_Area_Code { get; set; }
        public string? Manager_Sales_Area_Name { get; set; }
        public string? Manager_Sales_Area_Type { get; set; }
        public string? Manager_Employee_ID { get; set; }
        public string? Manager_Employee_Name { get; set; }
        public string? Manager_Email_Address { get; set; }
        public string? Rep_Sales_Force_Code { get; set; }
        public string? Rep_Sales_Force_Name { get; set; }
        public string? Rep_Sales_Area_Code { get; set; }
        public string? Rep_Sales_Area_Name { get; set; }
        public string? Rep_Sales_Area_Type { get; set; }
        public string? Rep_Employee_ID { get; set; }
        public string? Rep_Employee_Name { get; set; }
        public string? Rep_Email_Address { get; set; }
        public DateTime? Date_Entry { get; set; }
        public string? Event_Name { get; set; }
        public string? Product { get; set; }
        public int? Initiative_ID { get; set; }
        public string? Initiative { get; set; }
        public string? Note { get; set; }
        public string? Type { get; set; }
        public string? Event_Type { get; set; }
        public string? Attendance { get; set; }
        public string? Shared_Individual { get; set; }
        [Column(TypeName = "decimal(38,2)")]
        public decimal? Amount_Allocated { get; set; }
        public string? Customer_ID { get; set; }
        public string? Cust_Name_Display { get; set; }
        public int? Customer_Count { get; set; }
        public string? Customer_Type { get; set; }
        public string? FCPA_Veeva_ID { get; set; }
        public string? Account_ID { get; set; }
        public string? Account_Name { get; set; }
        public int? Tier { get; set; }
        public int? Status { get; set; }
        public string? Creator { get; set; }
        public DateTime? Created { get; set; }
        public string? Changer { get; set; }
        public DateTime? Changed { get; set; }
        public int? Is_Expired { get; set; }
        public int? Is_BRD { get; set; }
        public int? Is_Initiative { get; set; }
    }

    public class BudgetRepresentativeEditModel
    {
        public string? Int_Usr_ID { get; set; }
        public string? step { get; set; }
        public int? ID { get; set; }
        public string? Sales_Area_Code { get; set; }
        public string? Rep_Sales_Area_Code { get; set; }
        public DateTime? Date_Entry { get; set; }
        public string? Event_Name { get; set; }
        public string? Product { get; set; }
        public int? Initiative_ID { get; set; }
        public string? Note { get; set; }
        public string? Type { get; set; }
        public string? Event_Type { get; set; }
        public string? Attendance { get; set; }
        public string? Shared_Individual { get; set; }
        [Column(TypeName = "decimal(25, 2)")]
        public decimal? Amount_Allocated { get; set; }
        public string? Customer_ID { get; set; }
        public int? Customer_Count { get; set; }
        public string? Customer_Type { get; set; }
        public string? FCPA_Veeva_ID { get; set; }
        public string? Account_ID { get; set; }
        public int? Tier { get; set; }
        public int? Result { get; set; }
    }

}
