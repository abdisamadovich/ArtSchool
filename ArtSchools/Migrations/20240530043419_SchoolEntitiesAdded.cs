using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class SchoolEntitiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "schools");

            migrationBuilder.EnsureSchema(
                name: "school");

            migrationBuilder.AddColumn<int>(
                name: "school_id",
                schema: "identity",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "region",
                schema: "schools",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_oz = table.Column<string>(type: "text", nullable: true),
                    name_uz = table.Column<string>(type: "text", nullable: true),
                    name_ru = table.Column<string>(type: "text", nullable: true),
                    name_en = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_region", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "district",
                schema: "schools",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_oz = table.Column<string>(type: "text", nullable: true),
                    name_uz = table.Column<string>(type: "text", nullable: true),
                    name_ru = table.Column<string>(type: "text", nullable: true),
                    name_en = table.Column<string>(type: "text", nullable: true),
                    region_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district", x => x.id);
                    table.ForeignKey(
                        name: "FK_district_region_region_id",
                        column: x => x.region_id,
                        principalSchema: "schools",
                        principalTable: "region",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schools",
                schema: "schools",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: true),
                    name_oz = table.Column<string>(type: "text", nullable: true),
                    name_uz = table.Column<string>(type: "text", nullable: true),
                    name_ru = table.Column<string>(type: "text", nullable: true),
                    name_en = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    site_link = table.Column<string>(type: "text", nullable: true),
                    working_hours_start = table.Column<TimeSpan>(type: "interval", nullable: false),
                    working_hours_end = table.Column<TimeSpan>(type: "interval", nullable: false),
                    working_days_start = table.Column<int>(type: "integer", nullable: false),
                    working_days_end = table.Column<int>(type: "integer", nullable: false),
                    lunch_start = table.Column<TimeSpan>(type: "interval", nullable: false),
                    lunch_end = table.Column<TimeSpan>(type: "interval", nullable: false),
                    district_id = table.Column<int>(type: "integer", nullable: false),
                    region_id = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schools", x => x.id);
                    table.ForeignKey(
                        name: "FK_schools_district_district_id",
                        column: x => x.district_id,
                        principalSchema: "schools",
                        principalTable: "district",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schools_region_region_id",
                        column: x => x.region_id,
                        principalSchema: "schools",
                        principalTable: "region",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "books",
                schema: "schools",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    author = table.Column<string>(type: "text", nullable: true),
                    school_id = table.Column<int>(type: "integer", nullable: false),
                    file_url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.id);
                    table.ForeignKey(
                        name: "FK_books_schools_school_id",
                        column: x => x.school_id,
                        principalSchema: "schools",
                        principalTable: "schools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "directions",
                schema: "schools",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_oz = table.Column<string>(type: "text", nullable: true),
                    name_uz = table.Column<string>(type: "text", nullable: true),
                    name_ru = table.Column<string>(type: "text", nullable: true),
                    name_en = table.Column<string>(type: "text", nullable: true),
                    description_oz = table.Column<string>(type: "text", nullable: true),
                    description_uz = table.Column<string>(type: "text", nullable: true),
                    description_ru = table.Column<string>(type: "text", nullable: true),
                    description_en = table.Column<string>(type: "text", nullable: true),
                    school_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_directions", x => x.id);
                    table.ForeignKey(
                        name: "FK_directions_schools_school_id",
                        column: x => x.school_id,
                        principalSchema: "schools",
                        principalTable: "schools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                schema: "schools",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    position_oz = table.Column<string>(type: "text", nullable: true),
                    position_uz = table.Column<string>(type: "text", nullable: true),
                    position_ru = table.Column<string>(type: "text", nullable: true),
                    position_en = table.Column<string>(type: "text", nullable: true),
                    school_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                    table.ForeignKey(
                        name: "FK_employees_schools_school_id",
                        column: x => x.school_id,
                        principalSchema: "schools",
                        principalTable: "schools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events",
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
                    date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    file_url = table.Column<string>(type: "text", nullable: true),
                    school_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.id);
                    table.ForeignKey(
                        name: "FK_events_schools_school_id",
                        column: x => x.school_id,
                        principalSchema: "schools",
                        principalTable: "schools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "faqs",
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
                    school_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faqs", x => x.id);
                    table.ForeignKey(
                        name: "FK_faqs_schools_school_id",
                        column: x => x.school_id,
                        principalSchema: "schools",
                        principalTable: "schools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "galleries",
                schema: "school",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title_oz = table.Column<string>(type: "text", nullable: true),
                    title_uz = table.Column<string>(type: "text", nullable: true),
                    title_ru = table.Column<string>(type: "text", nullable: true),
                    title_en = table.Column<string>(type: "text", nullable: true),
                    file_url = table.Column<string>(type: "text", nullable: true),
                    school_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_galleries", x => x.id);
                    table.ForeignKey(
                        name: "FK_galleries_schools_school_id",
                        column: x => x.school_id,
                        principalSchema: "schools",
                        principalTable: "schools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "news",
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
                    file_url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_news", x => x.id);
                    table.ForeignKey(
                        name: "FK_news_schools_school_id",
                        column: x => x.school_id,
                        principalSchema: "schools",
                        principalTable: "schools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "top_student",
                schema: "schools",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    position_oz = table.Column<string>(type: "text", nullable: true),
                    position_uz = table.Column<string>(type: "text", nullable: true),
                    position_ru = table.Column<string>(type: "text", nullable: true),
                    position_en = table.Column<string>(type: "text", nullable: true),
                    school_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_top_student", x => x.id);
                    table.ForeignKey(
                        name: "FK_top_student_schools_school_id",
                        column: x => x.school_id,
                        principalSchema: "schools",
                        principalTable: "schools",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vacancies",
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
                    position_oz = table.Column<string>(type: "text", nullable: true),
                    position_uz = table.Column<string>(type: "text", nullable: true),
                    position_ru = table.Column<string>(type: "text", nullable: true),
                    position_en = table.Column<string>(type: "text", nullable: true),
                    requirements_oz = table.Column<string>(type: "text", nullable: true),
                    requirements_uz = table.Column<string>(type: "text", nullable: true),
                    requirements_ru = table.Column<string>(type: "text", nullable: true),
                    requirements_en = table.Column<string>(type: "text", nullable: true),
                    perks_oz = table.Column<string>(type: "text", nullable: true),
                    perks_uz = table.Column<string>(type: "text", nullable: true),
                    perks_ru = table.Column<string>(type: "text", nullable: true),
                    perks_en = table.Column<string>(type: "text", nullable: true),
                    school_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vacancies", x => x.id);
                    table.ForeignKey(
                        name: "FK_vacancies_schools_school_id",
                        column: x => x.school_id,
                        principalSchema: "schools",
                        principalTable: "schools",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_school_id",
                schema: "identity",
                table: "users",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_books_school_id",
                schema: "schools",
                table: "books",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_directions_school_id",
                schema: "schools",
                table: "directions",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_district_region_id",
                schema: "schools",
                table: "district",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_employees_school_id",
                schema: "schools",
                table: "employees",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_events_school_id",
                schema: "schools",
                table: "events",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_faqs_school_id",
                schema: "schools",
                table: "faqs",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_galleries_school_id",
                schema: "school",
                table: "galleries",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_news_school_id",
                schema: "schools",
                table: "news",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_schools_district_id",
                schema: "schools",
                table: "schools",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_schools_region_id",
                schema: "schools",
                table: "schools",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_top_student_school_id",
                schema: "schools",
                table: "top_student",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_vacancies_school_id",
                schema: "schools",
                table: "vacancies",
                column: "school_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_schools_school_id",
                schema: "identity",
                table: "users",
                column: "school_id",
                principalSchema: "schools",
                principalTable: "schools",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_schools_school_id",
                schema: "identity",
                table: "users");

            migrationBuilder.DropTable(
                name: "books",
                schema: "schools");

            migrationBuilder.DropTable(
                name: "directions",
                schema: "schools");

            migrationBuilder.DropTable(
                name: "employees",
                schema: "schools");

            migrationBuilder.DropTable(
                name: "events",
                schema: "schools");

            migrationBuilder.DropTable(
                name: "faqs",
                schema: "schools");

            migrationBuilder.DropTable(
                name: "galleries",
                schema: "school");

            migrationBuilder.DropTable(
                name: "news",
                schema: "schools");

            migrationBuilder.DropTable(
                name: "top_student",
                schema: "schools");

            migrationBuilder.DropTable(
                name: "vacancies",
                schema: "schools");

            migrationBuilder.DropTable(
                name: "schools",
                schema: "schools");

            migrationBuilder.DropTable(
                name: "district",
                schema: "schools");

            migrationBuilder.DropTable(
                name: "region",
                schema: "schools");

            migrationBuilder.DropIndex(
                name: "IX_users_school_id",
                schema: "identity",
                table: "users");

            migrationBuilder.DropColumn(
                name: "school_id",
                schema: "identity",
                table: "users");
        }
    }
}
