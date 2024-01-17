using System.ComponentModel.DataAnnotations.Schema;

namespace CanadaBIP_test.Models
{

    [Table("V_Budget_BR_Combobox_RepNames", Schema = "budget")]
    public class BudgetRepNameModel
    {
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Sales_Area_Code { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Sales_Area_Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Parent_Sales_Area_Code { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Parent_Sales_Area_Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Employee_Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Employee_ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Email_Address { get; set; }
        [Column(TypeName = "varchar(1)")]
        public string? Budget { get; set; }
        public int OrderColumn { get; set; }
    }

    [Table("V_Budget_Representative_Summary", Schema = "budget")]
    public class BudgetRepSummaryModel
    {
        public Int64 ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Product { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? BU { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? BU_NAME { get; set; }
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
        [Column(TypeName = "decimal(25, 2)")]
        public decimal Amount_Budget { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal Amount_Spent { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal Amount_Committed_Planned { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? Amount_Left { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal Amount_Wish { get; set; }
    }

    [Table("V_Budget_Representative", Schema = "budget")]
    public class BudgetRepresentativeModel
    {
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? BU { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? BU_NAME { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Manager_Sales_Area_Code { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Manager_Sales_Area_Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Manager_Sales_Area_Type { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Manager_Employee_ID { get; set; }
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
        [Column(TypeName = "nvarchar(1000)")]
        public string? Event_Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Product { get; set; }
        public int? Initiative_ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Initiative { get; set; }
        [Column(TypeName = "nvarchar(1000)")]
        public string? Note { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Type { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Event_Type { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Attendance { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Shared_Individual { get; set; }
        public decimal? Amount_Allocated { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Customer_ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Cust_Name_Display { get; set; }
        public int? Customer_Count { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Customer_Type { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? FCPA_Veeva_ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Account_ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Account_Name { get; set; }
        public int? Tier { get; set; }
        public int? Status { get; set; }
        [Column(TypeName = "nvarchar(255)")] 
        public string? Creator { get; set; }
        public DateTime? Created { get; set; }
        [Column(TypeName = "nvarchar(255)")] 
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
        public string? BU { get; set; }
        public string? Sales_Area_Code { get; set; }
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

    [Table("V_Budget_BR_M_Combobox_RepNames", Schema = "budget")]
    public class BudgetRepNameSelectModel
    {
        public int ID { get; set; }
        public string? Sales_Area_Code { get; set; }
        public string? Sales_Area_Name { get; set; }
        public string? Parent_Sales_Area_Code { get; set; }
        public string? Employee_Name { get; set; }
        public string? Employee_ID { get; set; }
        public string? Email_Address { get; set; }
    }

    [Table("V_Budget_BR_Combobox_Product", Schema = "budget")]
    public class BudgetRepProductModel
    {
        public int ID { get; set; }
        public string? Sales_Area_Code { get; set; }
        public string? Product { get; set; }
        public string? Product_UI { get; set; }
    }

    [Table("V_Budget_BR_Combobox_Initiatives", Schema = "budget")]
    public class BudgetRepInitiativeModel
    {
        public Int64 IDN { get; set; }
        public int? ID { get; set; }
        public string? BU { get; set; }
        public string? Product { get; set; }
        public string? Initiative { get; set; }
    }
}
