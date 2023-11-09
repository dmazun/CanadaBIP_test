using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanadaBIP_test.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "budget");

            migrationBuilder.CreateTable(
                name: "V_Budget_Manager",
                schema: "budget",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BU = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    BU_NAME = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Sales_Force_Code = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Sales_Force_Name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Sales_Area_Code = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Sales_Area_Name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Sales_Area_Type = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Employee_ID = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Employee_Name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Employee_Email = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Product = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Amount_Budget = table.Column<decimal>(type: "decimal(38,2)", nullable: true),
                    Amount_Allocated = table.Column<decimal>(type: "decimal(38,2)", nullable: false),
                    Amount_Left = table.Column<decimal>(type: "decimal(38,2)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Changer = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Changed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Is_BMD = table.Column<int>(type: "int", nullable: true),
                    Is_BMR = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_Budget_Manager", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "V_Budget_Manager",
                schema: "budget");
        }
    }
}
