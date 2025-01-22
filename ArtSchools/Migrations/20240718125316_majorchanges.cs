using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class majorchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file_url",
                schema: "schools",
                table: "galleries");

            migrationBuilder.RenameColumn(
                name: "file_url",
                schema: "schools",
                table: "news",
                newName: "image_url");

            migrationBuilder.RenameColumn(
                name: "file_url",
                schema: "schools",
                table: "events",
                newName: "image_url");

            migrationBuilder.AddColumn<int>(
                name: "file_id",
                schema: "schools",
                table: "top_student",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                schema: "schools",
                table: "top_student",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "image_id",
                schema: "schools",
                table: "news",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "gallery_id",
                schema: "schools",
                table: "file",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "image_id",
                schema: "schools",
                table: "events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "image_id",
                schema: "schools",
                table: "employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                schema: "schools",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "schools",
                table: "books",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "file_id",
                schema: "schools",
                table: "books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "image_id",
                schema: "schools",
                table: "books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                schema: "schools",
                table: "books",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "year",
                schema: "schools",
                table: "books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_top_student_file_id",
                schema: "schools",
                table: "top_student",
                column: "file_id");

            migrationBuilder.CreateIndex(
                name: "IX_news_image_id",
                schema: "schools",
                table: "news",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_file_gallery_id",
                schema: "schools",
                table: "file",
                column: "gallery_id");

            migrationBuilder.CreateIndex(
                name: "IX_events_image_id",
                schema: "schools",
                table: "events",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_employees_image_id",
                schema: "schools",
                table: "employees",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_books_file_id",
                schema: "schools",
                table: "books",
                column: "file_id");

            migrationBuilder.CreateIndex(
                name: "IX_books_image_id",
                schema: "schools",
                table: "books",
                column: "image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_books_file_file_id",
                schema: "schools",
                table: "books",
                column: "file_id",
                principalSchema: "schools",
                principalTable: "file",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_books_file_image_id",
                schema: "schools",
                table: "books",
                column: "image_id",
                principalSchema: "schools",
                principalTable: "file",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_file_image_id",
                schema: "schools",
                table: "employees",
                column: "image_id",
                principalSchema: "schools",
                principalTable: "file",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_events_file_image_id",
                schema: "schools",
                table: "events",
                column: "image_id",
                principalSchema: "schools",
                principalTable: "file",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_file_galleries_gallery_id",
                schema: "schools",
                table: "file",
                column: "gallery_id",
                principalSchema: "schools",
                principalTable: "galleries",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_news_file_image_id",
                schema: "schools",
                table: "news",
                column: "image_id",
                principalSchema: "schools",
                principalTable: "file",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_top_student_file_file_id",
                schema: "schools",
                table: "top_student",
                column: "file_id",
                principalSchema: "schools",
                principalTable: "file",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_file_file_id",
                schema: "schools",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_books_file_image_id",
                schema: "schools",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_file_image_id",
                schema: "schools",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_events_file_image_id",
                schema: "schools",
                table: "events");

            migrationBuilder.DropForeignKey(
                name: "FK_file_galleries_gallery_id",
                schema: "schools",
                table: "file");

            migrationBuilder.DropForeignKey(
                name: "FK_news_file_image_id",
                schema: "schools",
                table: "news");

            migrationBuilder.DropForeignKey(
                name: "FK_top_student_file_file_id",
                schema: "schools",
                table: "top_student");

            migrationBuilder.DropIndex(
                name: "IX_top_student_file_id",
                schema: "schools",
                table: "top_student");

            migrationBuilder.DropIndex(
                name: "IX_news_image_id",
                schema: "schools",
                table: "news");

            migrationBuilder.DropIndex(
                name: "IX_file_gallery_id",
                schema: "schools",
                table: "file");

            migrationBuilder.DropIndex(
                name: "IX_events_image_id",
                schema: "schools",
                table: "events");

            migrationBuilder.DropIndex(
                name: "IX_employees_image_id",
                schema: "schools",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "IX_books_file_id",
                schema: "schools",
                table: "books");

            migrationBuilder.DropIndex(
                name: "IX_books_image_id",
                schema: "schools",
                table: "books");

            migrationBuilder.DropColumn(
                name: "file_id",
                schema: "schools",
                table: "top_student");

            migrationBuilder.DropColumn(
                name: "image_url",
                schema: "schools",
                table: "top_student");

            migrationBuilder.DropColumn(
                name: "image_id",
                schema: "schools",
                table: "news");

            migrationBuilder.DropColumn(
                name: "gallery_id",
                schema: "schools",
                table: "file");

            migrationBuilder.DropColumn(
                name: "image_id",
                schema: "schools",
                table: "events");

            migrationBuilder.DropColumn(
                name: "image_id",
                schema: "schools",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "image_url",
                schema: "schools",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "description",
                schema: "schools",
                table: "books");

            migrationBuilder.DropColumn(
                name: "file_id",
                schema: "schools",
                table: "books");

            migrationBuilder.DropColumn(
                name: "image_id",
                schema: "schools",
                table: "books");

            migrationBuilder.DropColumn(
                name: "image_url",
                schema: "schools",
                table: "books");

            migrationBuilder.DropColumn(
                name: "year",
                schema: "schools",
                table: "books");

            migrationBuilder.RenameColumn(
                name: "image_url",
                schema: "schools",
                table: "news",
                newName: "file_url");

            migrationBuilder.RenameColumn(
                name: "image_url",
                schema: "schools",
                table: "events",
                newName: "file_url");

            migrationBuilder.AddColumn<string>(
                name: "file_url",
                schema: "schools",
                table: "galleries",
                type: "text",
                nullable: true);
        }
    }
}
