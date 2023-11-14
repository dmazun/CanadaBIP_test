using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanadaBIP_test.Migrations
{
    /// <inheritdoc />
    public partial class AddBudgetManagerDetailEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_sp_Update_Budget_Manager_Detail",
                schema: "budget",
                table: "sp_Update_Budget_Manager_Detail");

            migrationBuilder.AlterColumn<string>(
                name: "Step",
                schema: "budget",
                table: "sp_Update_Budget_Manager_Detail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date_Entry",
                schema: "budget",
                table: "sp_Update_Budget_Manager_Detail",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Budget_Manager_ID",
                schema: "budget",
                table: "sp_Update_Budget_Manager_Detail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount_Budget",
                schema: "budget",
                table: "sp_Update_Budget_Manager_Detail",
                type: "decimal(25,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(25,2)");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "budget",
                table: "sp_Update_Budget_Manager_Detail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Int_Usr_ID",
                schema: "budget",
                table: "sp_Update_Budget_Manager_Detail",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Int_Usr_ID",
                schema: "budget",
                table: "sp_Update_Budget_Manager_Detail");

            migrationBuilder.AlterColumn<string>(
                name: "Step",
                schema: "budget",
                table: "sp_Update_Budget_Manager_Detail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "budget",
                table: "sp_Update_Budget_Manager_Detail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date_Entry",
                schema: "budget",
                table: "sp_Update_Budget_Manager_Detail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Budget_Manager_ID",
                schema: "budget",
                table: "sp_Update_Budget_Manager_Detail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount_Budget",
                schema: "budget",
                table: "sp_Update_Budget_Manager_Detail",
                type: "decimal(25,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(25,2)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_sp_Update_Budget_Manager_Detail",
                schema: "budget",
                table: "sp_Update_Budget_Manager_Detail",
                column: "ID");
        }
    }
}
