using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class orderChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TicketsInOrder_orderId",
                table: "TicketsInOrder");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsInOrder_orderId",
                table: "TicketsInOrder",
                column: "orderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TicketsInOrder_orderId",
                table: "TicketsInOrder");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsInOrder_orderId",
                table: "TicketsInOrder",
                column: "orderId",
                unique: true);
        }
    }
}
