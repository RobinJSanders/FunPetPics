using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FunPetPics.Migrations
{
    public partial class UpdatePetPhotoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "PetPhotos");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUploaded",
                table: "PetPhotos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "PetPhotos",
                type: "nvarchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateUploaded",
                table: "PetPhotos");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "PetPhotos");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "PetPhotos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
