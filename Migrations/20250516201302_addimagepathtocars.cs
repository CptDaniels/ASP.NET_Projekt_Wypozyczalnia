using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_Projekt_Wypozyczalnia.Migrations
{
    /// <inheritdoc />
    public partial class addimagepathtocars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarPicture",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "CarPicture",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
