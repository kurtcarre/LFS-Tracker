using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LFS_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LfsInstance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstanceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<float>(type: "real", nullable: false),
                    LfsVersion = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LfsInstance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<float>(type: "real", nullable: false),
                    LfsVersion = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package", x => x.PackageId);
                });

            migrationBuilder.CreateTable(
                name: "LfsInstancePackage",
                columns: table => new
                {
                    InstalledInstancesId = table.Column<int>(type: "int", nullable: false),
                    InstalledPackagesPackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LfsInstancePackage", x => new { x.InstalledInstancesId, x.InstalledPackagesPackageId });
                    table.ForeignKey(
                        name: "FK_LfsInstancePackage_LfsInstance_InstalledInstancesId",
                        column: x => x.InstalledInstancesId,
                        principalTable: "LfsInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LfsInstancePackage_Package_InstalledPackagesPackageId",
                        column: x => x.InstalledPackagesPackageId,
                        principalTable: "Package",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LfsInstancePackage_InstalledPackagesPackageId",
                table: "LfsInstancePackage",
                column: "InstalledPackagesPackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LfsInstancePackage");

            migrationBuilder.DropTable(
                name: "LfsInstance");

            migrationBuilder.DropTable(
                name: "Package");
        }
    }
}
