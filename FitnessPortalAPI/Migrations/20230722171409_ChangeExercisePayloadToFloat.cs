using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessPortalAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeExercisePayloadToFloat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Payload",
                table: "Exercises",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Payload",
                table: "Exercises",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
