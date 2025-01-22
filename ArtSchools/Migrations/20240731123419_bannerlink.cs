using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class bannerlink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "link",
                schema: "schools",
                table: "banners",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "link",
                schema: "schools",
                table: "banners");
        }
    }
}
