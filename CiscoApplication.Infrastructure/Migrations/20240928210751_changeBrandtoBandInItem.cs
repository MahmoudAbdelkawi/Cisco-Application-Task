using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CiscoApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeBrandtoBandInItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "Band",
                table: "Items",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Band",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Items",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
