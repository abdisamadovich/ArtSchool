using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class majorfeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_head",
                schema: "schools",
                table: "employees",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "announcements",
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
                    image_id = table.Column<int>(type: "integer", nullable: false),
                    short_description_oz = table.Column<string>(type: "text", nullable: true),
                    short_description_uz = table.Column<string>(type: "text", nullable: true),
                    short_description_ru = table.Column<string>(type: "text", nullable: true),
                    short_description_en = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_announcements", x => x.id);
                    table.ForeignKey(
                        name: "FK_announcements_file_image_id",
                        column: x => x.image_id,
                        principalSchema: "schools",
                        principalTable: "file",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_announcements_schools_school_id",
                        column: x => x.school_id,
                        principalSchema: "schools",
                        principalTable: "schools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contact_us",
                schema: "schools",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "text", nullable: true),
                    lastname = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    message = table.Column<string>(type: "text", nullable: true),
                    school_id = table.Column<int>(type: "integer", nullable: false),
                    is_new = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_us", x => x.id);
                    table.ForeignKey(
                        name: "FK_contact_us_schools_school_id",
                        column: x => x.school_id,
                        principalSchema: "schools",
                        principalTable: "schools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pages",
                schema: "schools",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    url = table.Column<string>(type: "text", nullable: true),
                    name_oz = table.Column<string>(type: "text", nullable: true),
                    name_uz = table.Column<string>(type: "text", nullable: true),
                    name_ru = table.Column<string>(type: "text", nullable: true),
                    name_en = table.Column<string>(type: "text", nullable: true),
                    content_oz = table.Column<string>(type: "text", nullable: true),
                    content_uz = table.Column<string>(type: "text", nullable: true),
                    content_ru = table.Column<string>(type: "text", nullable: true),
                    content_en = table.Column<string>(type: "text", nullable: true),
                    school_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pages", x => x.id);
                    table.ForeignKey(
                        name: "FK_pages_schools_school_id",
                        column: x => x.school_id,
                        principalSchema: "schools",
                        principalTable: "schools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_announcements_image_id",
                schema: "schools",
                table: "announcements",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_announcements_school_id",
                schema: "schools",
                table: "announcements",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_us_school_id",
                schema: "schools",
                table: "contact_us",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_pages_school_id",
                schema: "schools",
                table: "pages",
                column: "school_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "announcements",
                schema: "schools");

            migrationBuilder.DropTable(
                name: "contact_us",
                schema: "schools");

            migrationBuilder.DropTable(
                name: "pages",
                schema: "schools");

            migrationBuilder.DropColumn(
                name: "is_head",
                schema: "schools",
                table: "employees");
        }
    }
}
