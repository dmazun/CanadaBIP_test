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
}
