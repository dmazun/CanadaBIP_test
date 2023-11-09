using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanadaBIP_test.Migrations
{
    /// <inheritdoc />
    public partial class AddBudgetManagerDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "V_Budget_Manager_Detail",
                schema: "budget",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Budget_Manager_ID = table.Column<int>(type: "int", nullable: true),
                    Date_Entry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Amount_Budget = table.Column<decimal>(type: "decimal(25,2)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Changer = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Changed = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_Budget_Manager_Detail", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "V_Budget_Manager_Detail",
                schema: "budget");
        }
    }
}
