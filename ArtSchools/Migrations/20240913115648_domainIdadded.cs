using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class domainIdadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "domain_id",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "number",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "schools",
                table: "schools",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "domain_id", "number" },
                values: new object[] { null, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "domain_id",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "number",
                schema: "schools",
                table: "schools");
        }
    }
}
