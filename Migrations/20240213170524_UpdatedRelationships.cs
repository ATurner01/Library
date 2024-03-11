using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Books",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_AccountId",
                table: "Books",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Accounts_AccountId",
                table: "Books",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Accounts_AccountId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AccountId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Books");
        }
    }
}
