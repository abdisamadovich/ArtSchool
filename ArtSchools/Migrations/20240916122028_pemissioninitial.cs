using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class pemissioninitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "identity",
                table: "permissions",
                columns: new[] { "id", "permission_name" },
                values: new object[] { 1, "SCHOOL_ACTIONS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "permissions",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
