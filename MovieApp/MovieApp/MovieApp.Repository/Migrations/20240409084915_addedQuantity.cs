using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addedQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "TicketsInOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "TicketsInOrder");
        }
    }
}
