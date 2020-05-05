using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerApi.Migrations
{
    public partial class AddedAbv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Abv",
                table: "Beers",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abv",
                table: "Beers");
        }
    }
}
