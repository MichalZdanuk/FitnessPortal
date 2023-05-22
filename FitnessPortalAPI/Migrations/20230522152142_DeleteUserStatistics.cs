using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessPortalAPI.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUserStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BMIs_UserStatistics_UserStatisticsId",
                table: "BMIs");

            migrationBuilder.DropTable(
                name: "UserStatistics");

            migrationBuilder.RenameColumn(
                name: "UserStatisticsId",
                table: "BMIs",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BMIs_UserStatisticsId",
                table: "BMIs",
                newName: "IX_BMIs_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BMIs_Users_UserId",
                table: "BMIs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BMIs_Users_UserId",
                table: "BMIs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BMIs",
                newName: "UserStatisticsId");

            migrationBuilder.RenameIndex(
                name: "IX_BMIs_UserId",
                table: "BMIs",
                newName: "IX_BMIs_UserStatisticsId");

            migrationBuilder.CreateTable(
                name: "UserStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NumberOfTrainings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStatistics_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_UserId",
                table: "UserStatistics",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BMIs_UserStatistics_UserStatisticsId",
                table: "BMIs",
                column: "UserStatisticsId",
                principalTable: "UserStatistics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
