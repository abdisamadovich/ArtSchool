using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class schoolroleinitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "identity",
                table: "permissions",
                keyColumn: "id",
                keyValue: 1,
                column: "permission_name",
                value: "SCHOOL_ACTIONS");

            migrationBuilder.InsertData(
                schema: "identity",
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[] { 1, "School admin" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "role_permission",
                columns: new[] { "permission_id", "role_id" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "roles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "permissions",
                keyColumn: "id",
                keyValue: 1,
                column: "permission_name",
                value: "SCHOOL");
        }
    }
}
