using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class vacancies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vacancies_schools_school_id",
                schema: "schools",
                table: "vacancies");

            migrationBuilder.AlterColumn<int>(
                name: "school_id",
                schema: "schools",
                table: "vacancies",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_vacancies_schools_school_id",
                schema: "schools",
                table: "vacancies",
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
                name: "FK_vacancies_schools_school_id",
                schema: "schools",
                table: "vacancies");

            migrationBuilder.AlterColumn<int>(
                name: "school_id",
                schema: "schools",
                table: "vacancies",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_vacancies_schools_school_id",
                schema: "schools",
                table: "vacancies",
                column: "school_id",
                principalSchema: "schools",
                principalTable: "schools",
                principalColumn: "id");
        }
    }
}
