using System.ComponentModel.DataAnnotations.Schema;

namespace CanadaBIP_test.Models
{
    [Table("V_Budget_Manager", Schema = "budget")]
    public class BudgetManagerViewModel
    {
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? BU { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? BU_NAME { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Sales_Force_Code { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Sales_Force_Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Sales_Area_Code { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Sales_Area_Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Sales_Area_Type { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Employee_ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Employee_Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Employee_Email { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Product { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? Amount_Budget { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal Amount_Allocated { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? Amount_Left { get; set; }
        public int? Status { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Creator { get; set; }
        public DateTime? Created { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Changer { get; set; }
        public DateTime? Changed { get; set; }
        public int? Is_BMD { get; set; }
        public int? Is_BMR { get; set; }
    }
}
