using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessPortalAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInBMIEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Width",
                table: "BMIs",
                newName: "Weight");

            migrationBuilder.AlterColumn<float>(
                name: "BMIScore",
                table: "BMIs",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "BMICategory",
                table: "BMIs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BMICategory",
                table: "BMIs");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "BMIs",
                newName: "Width");

            migrationBuilder.AlterColumn<int>(
                name: "BMIScore",
                table: "BMIs",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
