using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class eventdirection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "direction_id",
                schema: "schools",
                table: "events",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "image_id",
                schema: "schools",
                table: "directions",
                type: "integer",
                nullable: true,
                defaultValue: null);

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                schema: "schools",
                table: "directions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_events_direction_id",
                schema: "schools",
                table: "events",
                column: "direction_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_directions_image_id",
                schema: "schools",
                table: "directions",
                column: "image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_directions_file_image_id",
                schema: "schools",
                table: "directions",
                column: "image_id",
                principalSchema: "schools",
                principalTable: "file",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_events_directions_direction_id",
                schema: "schools",
                table: "events",
                column: "direction_id",
                principalSchema: "schools",
                principalTable: "directions",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_directions_file_image_id",
                schema: "schools",
                table: "directions");

            migrationBuilder.DropForeignKey(
                name: "FK_events_directions_direction_id",
                schema: "schools",
                table: "events");

            migrationBuilder.DropIndex(
                name: "IX_events_direction_id",
                schema: "schools",
                table: "events");

            migrationBuilder.DropIndex(
                name: "IX_directions_image_id",
                schema: "schools",
                table: "directions");

            migrationBuilder.DropColumn(
                name: "direction_id",
                schema: "schools",
                table: "events");

            migrationBuilder.DropColumn(
                name: "image_id",
                schema: "schools",
                table: "directions");

            migrationBuilder.DropColumn(
                name: "image_url",
                schema: "schools",
                table: "directions");
        }
    }
}
