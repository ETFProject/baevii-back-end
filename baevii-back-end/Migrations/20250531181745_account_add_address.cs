using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace baevii_back_end.Migrations
{
    /// <inheritdoc />
    public partial class account_add_address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "accounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "accounts");
        }
    }
}
