using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace baevii_back_end.Migrations
{
    /// <inheritdoc />
    public partial class user_unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PrivyId",
                table: "users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_PrivyId",
                table: "users",
                column: "PrivyId",
                unique: true,
                filter: "[PrivyId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_PrivyId",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "PrivyId",
                table: "users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
