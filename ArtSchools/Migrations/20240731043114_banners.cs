using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class banners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "banners",
                schema: "schools",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title_oz = table.Column<string>(type: "text", nullable: true),
                    title_uz = table.Column<string>(type: "text", nullable: true),
                    title_ru = table.Column<string>(type: "text", nullable: true),
                    title_en = table.Column<string>(type: "text", nullable: true),
                    description_oz = table.Column<string>(type: "text", nullable: true),
                    description_uz = table.Column<string>(type: "text", nullable: true),
                    description_ru = table.Column<string>(type: "text", nullable: true),
                    description_en = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_published = table.Column<bool>(type: "boolean", nullable: false),
                    school_id = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    image_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banners", x => x.id);
                    table.ForeignKey(
                        name: "FK_banners_file_image_id",
                        column: x => x.image_id,
                        principalSchema: "schools",
                        principalTable: "file",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_banners_schools_school_id",
                        column: x => x.school_id,
                        principalSchema: "schools",
                        principalTable: "schools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_banners_image_id",
                schema: "schools",
                table: "banners",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_banners_school_id",
                schema: "schools",
                table: "banners",
                column: "school_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "banners",
                schema: "schools");
        }
    }
}
