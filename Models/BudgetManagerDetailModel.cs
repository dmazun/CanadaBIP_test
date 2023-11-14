using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CanadaBIP_test.Models
{
    [Table("V_Budget_Manager_Detail", Schema = "budget")]
    public class BudgetManagerDetailViewModel
    {
        public int ID { get; set; }
        public int? Budget_Manager_ID { get; set; }
        public DateTime? Date_Entry { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Type { get; set; }
        [Column(TypeName = "decimal(25, 2)")]
        public decimal? Amount_Budget { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Comment { get; set; }
        public int? Status { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Creator { get; set; }
        public DateTime? Created { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Changer { get; set; }
        public DateTime? Changed { get; set; }
    }

    [Keyless]
    public class BudgetManagerDetailEditModel
    {
        public int? ID { get; set; }
        public string? Int_Usr_ID { get; set; }
        public string? Step { get; set; }
        public int? Budget_Manager_ID { get; set; }
        public DateTime? Date_Entry { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Type { get; set; }
        [Column(TypeName = "decimal(25, 2)")]
        public decimal? Amount_Budget { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Comment { get; set; }
    }

    [Keyless]
    public class BudgetManagerDetailEditOut
    {
        public int Results { get; set; } = 0;
    }
}
