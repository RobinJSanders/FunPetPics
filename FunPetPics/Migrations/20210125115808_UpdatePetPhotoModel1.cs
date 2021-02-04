using Microsoft.EntityFrameworkCore.Migrations;

namespace FunPetPics.Migrations
{
    public partial class UpdatePetPhotoModel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PetPhotos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PetPhotos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PetPhotos");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "PetPhotos");
        }
    }
}
