using Microsoft.EntityFrameworkCore.Migrations;

namespace FunPetPics.Migrations
{
    public partial class UpdatePetPhotoModel4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetPhotos_Users_UserModelId",
                table: "PetPhotos");

            migrationBuilder.AlterColumn<int>(
                name: "UserModelId",
                table: "PetPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PetPhotos_Users_UserModelId",
                table: "PetPhotos",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetPhotos_Users_UserModelId",
                table: "PetPhotos");

            migrationBuilder.AlterColumn<int>(
                name: "UserModelId",
                table: "PetPhotos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PetPhotos_Users_UserModelId",
                table: "PetPhotos",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
