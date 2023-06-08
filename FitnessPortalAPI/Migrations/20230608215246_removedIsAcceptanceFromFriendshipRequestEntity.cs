using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessPortalAPI.Migrations
{
    /// <inheritdoc />
    public partial class removedIsAcceptanceFromFriendshipRequestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "FriendshipRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "FriendshipRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
