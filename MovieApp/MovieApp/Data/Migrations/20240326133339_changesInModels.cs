using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class changesInModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TicketInOrders_orderId",
                table: "TicketInOrders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_userId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_TicketInOrders_orderId",
                table: "TicketInOrders",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_userId",
                table: "Orders",
                column: "userId",
                unique: true,
                filter: "[userId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TicketInOrders_orderId",
                table: "TicketInOrders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_userId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_TicketInOrders_orderId",
                table: "TicketInOrders",
                column: "orderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_userId",
                table: "Orders",
                column: "userId");
        }
    }
}
