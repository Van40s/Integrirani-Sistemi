using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class attributeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_AspNetUsers_UserId",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_concerts_concertTickets",
                table: "tickets");

            migrationBuilder.DropIndex(
                name: "IX_tickets_UserId",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tickets");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "tickets",
                newName: "numberOfPeople");

            migrationBuilder.RenameColumn(
                name: "concertTickets",
                table: "tickets",
                newName: "concertid");

            migrationBuilder.RenameIndex(
                name: "IX_tickets_concertTickets",
                table: "tickets",
                newName: "IX_tickets_concertid");

            migrationBuilder.RenameColumn(
                name: "groupName",
                table: "concerts",
                newName: "concertName");

            migrationBuilder.AddColumn<string>(
                name: "forUserId",
                table: "tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "concertPrice",
                table: "concerts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tickets_forUserId",
                table: "tickets",
                column: "forUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_AspNetUsers_forUserId",
                table: "tickets",
                column: "forUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_concerts_concertid",
                table: "tickets",
                column: "concertid",
                principalTable: "concerts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_AspNetUsers_forUserId",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_concerts_concertid",
                table: "tickets");

            migrationBuilder.DropIndex(
                name: "IX_tickets_forUserId",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "forUserId",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "concertPrice",
                table: "concerts");

            migrationBuilder.RenameColumn(
                name: "numberOfPeople",
                table: "tickets",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "concertid",
                table: "tickets",
                newName: "concertTickets");

            migrationBuilder.RenameIndex(
                name: "IX_tickets_concertid",
                table: "tickets",
                newName: "IX_tickets_concertTickets");

            migrationBuilder.RenameColumn(
                name: "concertName",
                table: "concerts",
                newName: "groupName");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "tickets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_UserId",
                table: "tickets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_AspNetUsers_UserId",
                table: "tickets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_concerts_concertTickets",
                table: "tickets",
                column: "concertTickets",
                principalTable: "concerts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
