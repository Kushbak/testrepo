using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceManagmentApplication.DAL.Migrations
{
    public partial class secondmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_CounterParties_CounterPartyId",
                table: "Scores");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Scores_Score1Id",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Scores_Score2Id",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_Score1Id",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_Score2Id",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Scores_CounterPartyId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Score1Id",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Score2Id",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CounterPartyId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Money",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "ScoreNumber",
                table: "Scores");

            migrationBuilder.AddColumn<int>(
                name: "CounterPartyId",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScoreId",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Balance",
                table: "Scores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Scores",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Scores",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CounterPartyId",
                table: "Transactions",
                column: "CounterPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ScoreId",
                table: "Transactions",
                column: "ScoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CounterParties_CounterPartyId",
                table: "Transactions",
                column: "CounterPartyId",
                principalTable: "CounterParties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Scores_ScoreId",
                table: "Transactions",
                column: "ScoreId",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CounterParties_CounterPartyId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Scores_ScoreId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CounterPartyId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ScoreId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CounterPartyId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ScoreId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Scores");

            migrationBuilder.AddColumn<int>(
                name: "Score1Id",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Score2Id",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CounterPartyId",
                table: "Scores",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Money",
                table: "Scores",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ScoreNumber",
                table: "Scores",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Score1Id",
                table: "Transactions",
                column: "Score1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Score2Id",
                table: "Transactions",
                column: "Score2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_CounterPartyId",
                table: "Scores",
                column: "CounterPartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_CounterParties_CounterPartyId",
                table: "Scores",
                column: "CounterPartyId",
                principalTable: "CounterParties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Scores_Score1Id",
                table: "Transactions",
                column: "Score1Id",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Scores_Score2Id",
                table: "Transactions",
                column: "Score2Id",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
