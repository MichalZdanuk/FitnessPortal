using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessPortalAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedShortDescriptionToArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Articles");
        }
    }
}
