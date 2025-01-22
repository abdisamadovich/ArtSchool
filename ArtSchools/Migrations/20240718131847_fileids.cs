using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class fileids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_top_student_file_file_id",
                schema: "schools",
                table: "top_student");

            migrationBuilder.RenameColumn(
                name: "file_id",
                schema: "schools",
                table: "top_student",
                newName: "image_id");

            migrationBuilder.RenameIndex(
                name: "IX_top_student_file_id",
                schema: "schools",
                table: "top_student",
                newName: "IX_top_student_image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_top_student_file_image_id",
                schema: "schools",
                table: "top_student",
                column: "image_id",
                principalSchema: "schools",
                principalTable: "file",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_top_student_file_image_id",
                schema: "schools",
                table: "top_student");

            migrationBuilder.RenameColumn(
                name: "image_id",
                schema: "schools",
                table: "top_student",
                newName: "file_id");

            migrationBuilder.RenameIndex(
                name: "IX_top_student_image_id",
                schema: "schools",
                table: "top_student",
                newName: "IX_top_student_file_id");

            migrationBuilder.AddForeignKey(
                name: "FK_top_student_file_file_id",
                schema: "schools",
                table: "top_student",
                column: "file_id",
                principalSchema: "schools",
                principalTable: "file",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
