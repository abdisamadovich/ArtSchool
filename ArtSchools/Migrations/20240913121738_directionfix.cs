using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class directionfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_directions_file_image_id",
                schema: "schools",
                table: "directions");

            migrationBuilder.AlterColumn<int>(
                name: "image_id",
                schema: "schools",
                table: "directions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_directions_file_image_id",
                schema: "schools",
                table: "directions",
                column: "image_id",
                principalSchema: "schools",
                principalTable: "file",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_directions_file_image_id",
                schema: "schools",
                table: "directions");

            migrationBuilder.AlterColumn<int>(
                name: "image_id",
                schema: "schools",
                table: "directions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_directions_file_image_id",
                schema: "schools",
                table: "directions",
                column: "image_id",
                principalSchema: "schools",
                principalTable: "file",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
