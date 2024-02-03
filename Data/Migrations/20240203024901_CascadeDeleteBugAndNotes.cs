using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugBuddy.Data.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteBugAndNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Bug_BugId",
                table: "Note");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Bug_BugId",
                table: "Note",
                column: "BugId",
                principalTable: "Bug",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Bug_BugId",
                table: "Note");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Bug_BugId",
                table: "Note",
                column: "BugId",
                principalTable: "Bug",
                principalColumn: "Id");
        }
    }
}
