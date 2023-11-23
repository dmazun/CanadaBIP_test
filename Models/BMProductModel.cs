using System.ComponentModel.DataAnnotations.Schema;

namespace CanadaBIP_test.Models
{
    [Table("V_Budget_BM_Combobox_Product", Schema = "budget")]
    public class BMProductModel
    {
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Sales_Area_Code { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Product { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Product_UI { get; set; }
    }
}
