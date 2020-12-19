using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceManagmentApplication.DAL.Migrations
{
    public partial class lastmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Scores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Projects",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "PaymentTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "OperationTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Operations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "FinanceActions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "CounterParties",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "OperationTypes");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "FinanceActions");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "CounterParties");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "AspNetRoles");
        }
    }
}
