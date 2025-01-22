using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class shortDescriptionadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "short_description_en",
                schema: "schools",
                table: "news",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_oz",
                schema: "schools",
                table: "news",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_ru",
                schema: "schools",
                table: "news",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_uz",
                schema: "schools",
                table: "news",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_en",
                schema: "schools",
                table: "events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_oz",
                schema: "schools",
                table: "events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_ru",
                schema: "schools",
                table: "events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_uz",
                schema: "schools",
                table: "events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_en",
                schema: "schools",
                table: "banners",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_oz",
                schema: "schools",
                table: "banners",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_ru",
                schema: "schools",
                table: "banners",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_description_uz",
                schema: "schools",
                table: "banners",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "short_description_en",
                schema: "schools",
                table: "news");

            migrationBuilder.DropColumn(
                name: "short_description_oz",
                schema: "schools",
                table: "news");

            migrationBuilder.DropColumn(
                name: "short_description_ru",
                schema: "schools",
                table: "news");

            migrationBuilder.DropColumn(
                name: "short_description_uz",
                schema: "schools",
                table: "news");

            migrationBuilder.DropColumn(
                name: "short_description_en",
                schema: "schools",
                table: "events");

            migrationBuilder.DropColumn(
                name: "short_description_oz",
                schema: "schools",
                table: "events");

            migrationBuilder.DropColumn(
                name: "short_description_ru",
                schema: "schools",
                table: "events");

            migrationBuilder.DropColumn(
                name: "short_description_uz",
                schema: "schools",
                table: "events");

            migrationBuilder.DropColumn(
                name: "short_description_en",
                schema: "schools",
                table: "banners");

            migrationBuilder.DropColumn(
                name: "short_description_oz",
                schema: "schools",
                table: "banners");

            migrationBuilder.DropColumn(
                name: "short_description_ru",
                schema: "schools",
                table: "banners");

            migrationBuilder.DropColumn(
                name: "short_description_uz",
                schema: "schools",
                table: "banners");
        }
    }
}
