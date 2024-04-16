using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class newModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tickets",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Movies",
                newName: "id");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketsInOrder",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ticketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    orderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketsInOrder", x => x.id);
                    table.ForeignKey(
                        name: "FK_TicketsInOrder_Orders_orderId",
                        column: x => x.orderId,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketsInOrder_Tickets_ticketId",
                        column: x => x.ticketId,
                        principalTable: "Tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_userId",
                table: "Orders",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketsInOrder_orderId",
                table: "TicketsInOrder",
                column: "orderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketsInOrder_ticketId",
                table: "TicketsInOrder",
                column: "ticketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketsInOrder");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Tickets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Movies",
                newName: "Id");
        }
    }
}
