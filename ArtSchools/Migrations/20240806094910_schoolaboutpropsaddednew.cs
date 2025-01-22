using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class schoolaboutpropsaddednew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "boy_count",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "girl_count",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                schema: "schools",
                table: "schools",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "schools",
                table: "schools",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "boy_count", "girl_count", "phone_number" },
                values: new object[] { 0, 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "boy_count",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "girl_count",
                schema: "schools",
                table: "schools");

            migrationBuilder.DropColumn(
                name: "phone_number",
                schema: "schools",
                table: "schools");
        }
    }
}
