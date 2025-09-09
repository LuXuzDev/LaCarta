using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaCartaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRestaurantTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int[]>(
                name: "RestaurantTags",
                table: "Restaurants",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantTags",
                table: "Restaurants");
        }
    }
}
