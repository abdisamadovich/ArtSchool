using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class schoolaboutpropsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "age_lvl_five",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "age_lvl_four",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "age_lvl_one",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "age_lvl_six",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "age_lvl_three",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "age_lvl_two",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "class_count",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "description_en",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description_oz",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description_ru",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description_uz",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "facebook_link",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "image_id",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "instagram_link",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "map",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_en",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_oz",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_ru",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_uz",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "special_class_count",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "student_count",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "teacher_count",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "telegram_link",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "video_link",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "years",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "youtube_link",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "schools",
                table: "schools",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "age_lvl_five", "age_lvl_four", "age_lvl_one", "age_lvl_six", "age_lvl_three", "age_lvl_two", "class_count", "description_en", "description_oz", "description_ru", "description_uz", "facebook_link", "image_id", "image_url", "instagram_link", "map", "short_description_en", "short_description_oz", "short_description_ru", "short_description_uz", "special_class_count", "student_count", "teacher_count", "telegram_link", "video_link", "years", "youtube_link" },
                values: new object[] { 0, 0, 0, 0, 0, 0, 0, null, null, null, null, null, null, null, null, null, null, null, null, null, 0, 0, 0, null, null, 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_schools_image_id",
                schema: "schools",
                table: "schools",
                column: "image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_schools_file_image_id",
                schema: "schools",
                table: "schools",
                column: "image_id",
                principalSchema: "schools",
                principalTable: "file",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schools_file_image_id",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropIndex(
                name: "IX_schools_image_id",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "age_lvl_five",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "age_lvl_four",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "age_lvl_one",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "age_lvl_six",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "age_lvl_three",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "age_lvl_two",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "class_count",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "description_en",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "description_oz",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "description_ru",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "description_uz",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "facebook_link",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "image_id",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "image_url",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "instagram_link",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "map",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "short_description_en",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "short_description_oz",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "short_description_ru",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "short_description_uz",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "special_class_count",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "student_count",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "teacher_count",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "telegram_link",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "video_link",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "years",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "youtube_link",
                schema: "schools",
                table: "schools");
        }
    }
}
