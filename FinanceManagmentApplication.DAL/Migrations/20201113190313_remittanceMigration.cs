using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceManagmentApplication.DAL.Migrations
{
    public partial class remittanceMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CounterParties_CounterPartyId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Operations_OperationId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Projects_ProjectId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Scores_ScoreId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_UserId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "FinanceActions");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_UserId",
                table: "FinanceActions",
                newName: "IX_FinanceActions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_ScoreId",
                table: "FinanceActions",
                newName: "IX_FinanceActions_ScoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_ProjectId",
                table: "FinanceActions",
                newName: "IX_FinanceActions_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_OperationId",
                table: "FinanceActions",
                newName: "IX_FinanceActions_OperationId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CounterPartyId",
                table: "FinanceActions",
                newName: "IX_FinanceActions_CounterPartyId");

            migrationBuilder.AlterColumn<int>(
                name: "CounterPartyId",
                table: "FinanceActions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActionDate",
                table: "FinanceActions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "FinanceActions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Score2Id",
                table: "FinanceActions",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinanceActions",
                table: "FinanceActions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceActions_Score2Id",
                table: "FinanceActions",
                column: "Score2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceActions_Operations_OperationId",
                table: "FinanceActions",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceActions_Projects_ProjectId",
                table: "FinanceActions",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceActions_Scores_ScoreId",
                table: "FinanceActions",
                column: "ScoreId",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceActions_AspNetUsers_UserId",
                table: "FinanceActions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceActions_Scores_Score2Id",
                table: "FinanceActions",
                column: "Score2Id",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceActions_CounterParties_CounterPartyId",
                table: "FinanceActions",
                column: "CounterPartyId",
                principalTable: "CounterParties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinanceActions_Operations_OperationId",
                table: "FinanceActions");

            migrationBuilder.DropForeignKey(
                name: "FK_FinanceActions_Projects_ProjectId",
                table: "FinanceActions");

            migrationBuilder.DropForeignKey(
                name: "FK_FinanceActions_Scores_ScoreId",
                table: "FinanceActions");

            migrationBuilder.DropForeignKey(
                name: "FK_FinanceActions_AspNetUsers_UserId",
                table: "FinanceActions");

            migrationBuilder.DropForeignKey(
                name: "FK_FinanceActions_Scores_Score2Id",
                table: "FinanceActions");

            migrationBuilder.DropForeignKey(
                name: "FK_FinanceActions_CounterParties_CounterPartyId",
                table: "FinanceActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinanceActions",
                table: "FinanceActions");

            migrationBuilder.DropIndex(
                name: "IX_FinanceActions_Score2Id",
                table: "FinanceActions");

            migrationBuilder.DropColumn(
                name: "ActionDate",
                table: "FinanceActions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "FinanceActions");

            migrationBuilder.DropColumn(
                name: "Score2Id",
                table: "FinanceActions");

            migrationBuilder.RenameTable(
                name: "FinanceActions",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_FinanceActions_CounterPartyId",
                table: "Transactions",
                newName: "IX_Transactions_CounterPartyId");

            migrationBuilder.RenameIndex(
                name: "IX_FinanceActions_UserId",
                table: "Transactions",
                newName: "IX_Transactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FinanceActions_ScoreId",
                table: "Transactions",
                newName: "IX_Transactions_ScoreId");

            migrationBuilder.RenameIndex(
                name: "IX_FinanceActions_ProjectId",
                table: "Transactions",
                newName: "IX_Transactions_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_FinanceActions_OperationId",
                table: "Transactions",
                newName: "IX_Transactions_OperationId");

            migrationBuilder.AlterColumn<int>(
                name: "CounterPartyId",
                table: "Transactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Transactions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CounterParties_CounterPartyId",
                table: "Transactions",
                column: "CounterPartyId",
                principalTable: "CounterParties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Operations_OperationId",
                table: "Transactions",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Projects_ProjectId",
                table: "Transactions",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Scores_ScoreId",
                table: "Transactions",
                column: "ScoreId",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
