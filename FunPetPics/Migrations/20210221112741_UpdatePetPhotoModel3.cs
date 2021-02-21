using Microsoft.EntityFrameworkCore.Migrations;

namespace FunPetPics.Migrations
{
    public partial class UpdatePetPhotoModel3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UploadedBy",
                table: "PetPhotos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedBy",
                table: "PetPhotos");
        }
    }
}
