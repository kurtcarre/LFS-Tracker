using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LFS_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class AddCorePackageProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCoreLfsPackage",
                table: "Package",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCoreLfsPackage",
                table: "Package");
        }
    }
}
