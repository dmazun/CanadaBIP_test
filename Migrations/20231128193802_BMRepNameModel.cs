using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanadaBIP_test.Migrations
{
    /// <inheritdoc />
    public partial class BMRepNameModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "V_Budget_BMR_Combobox_Product",
                schema: "budget",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sales_Area_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_Budget_BMR_Combobox_Product", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "V_Budget_BMR_Combobox_RepNames",
                schema: "budget",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sales_Area_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sales_Area_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parent_Sales_Area_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_Budget_BMR_Combobox_RepNames", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "V_Budget_BMR_Combobox_Product",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "V_Budget_BMR_Combobox_RepNames",
                schema: "budget");
        }
    }
}
