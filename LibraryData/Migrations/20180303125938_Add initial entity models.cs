using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryData.Migrations
{
    public partial class Addinitialentitymodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "HomeLibraryBranchId",
                "Patrons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "LibraryCardId",
                "Patrons",
                nullable: true);

            migrationBuilder.CreateTable(
                "LibraryBranches",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: false),
                    Descritpion = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    OpenDate = table.Column<DateTime>(nullable: false),
                    Telephone = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_LibraryBranches", x => x.Id); });

            migrationBuilder.CreateTable(
                "LibraryCards",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Fees = table.Column<decimal>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_LibraryCards", x => x.Id); });

            migrationBuilder.CreateTable(
                "Statuses",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Statuses", x => x.Id); });

            migrationBuilder.CreateTable(
                "BranchHours",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(nullable: true),
                    CloseTime = table.Column<int>(nullable: false),
                    DayOfWeek = table.Column<int>(nullable: false),
                    OpenTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchHours", x => x.Id);
                    table.ForeignKey(
                        "FK_BranchHours_LibraryBranches_BranchId",
                        x => x.BranchId,
                        "LibraryBranches",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "LibraryAssets",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Cost = table.Column<decimal>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: true),
                    NumberOfCopies = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    DeweyIndex = table.Column<string>(nullable: true),
                    ISBN = table.Column<string>(nullable: true),
                    Director = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryAssets", x => x.Id);
                    table.ForeignKey(
                        "FK_LibraryAssets_LibraryBranches_LocationId",
                        x => x.LocationId,
                        "LibraryBranches",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_LibraryAssets_Statuses_StatusId",
                        x => x.StatusId,
                        "Statuses",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Checkouts",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    LibraryAssetId = table.Column<int>(nullable: false),
                    LibraryCardId = table.Column<int>(nullable: true),
                    Since = table.Column<DateTime>(nullable: false),
                    Until = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkouts", x => x.Id);
                    table.ForeignKey(
                        "FK_Checkouts_LibraryAssets_LibraryAssetId",
                        x => x.LibraryAssetId,
                        "LibraryAssets",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Checkouts_LibraryCards_LibraryCardId",
                        x => x.LibraryCardId,
                        "LibraryCards",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "CheckoutHistories",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckedIn = table.Column<DateTime>(nullable: true),
                    CheckedOut = table.Column<DateTime>(nullable: false),
                    LibraryAssetId = table.Column<int>(nullable: false),
                    LibraryCardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutHistories", x => x.Id);
                    table.ForeignKey(
                        "FK_CheckoutHistories_LibraryAssets_LibraryAssetId",
                        x => x.LibraryAssetId,
                        "LibraryAssets",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_CheckoutHistories_LibraryCards_LibraryCardId",
                        x => x.LibraryCardId,
                        "LibraryCards",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Holds",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    HoldPlaced = table.Column<DateTime>(nullable: false),
                    LibraryAssetId = table.Column<int>(nullable: true),
                    LibraryCardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holds", x => x.Id);
                    table.ForeignKey(
                        "FK_Holds_LibraryAssets_LibraryAssetId",
                        x => x.LibraryAssetId,
                        "LibraryAssets",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Holds_LibraryCards_LibraryCardId",
                        x => x.LibraryCardId,
                        "LibraryCards",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Patrons_HomeLibraryBranchId",
                "Patrons",
                "HomeLibraryBranchId");

            migrationBuilder.CreateIndex(
                "IX_Patrons_LibraryCardId",
                "Patrons",
                "LibraryCardId");

            migrationBuilder.CreateIndex(
                "IX_BranchHours_BranchId",
                "BranchHours",
                "BranchId");

            migrationBuilder.CreateIndex(
                "IX_Checkouts_LibraryAssetId",
                "Checkouts",
                "LibraryAssetId");

            migrationBuilder.CreateIndex(
                "IX_Checkouts_LibraryCardId",
                "Checkouts",
                "LibraryCardId");

            migrationBuilder.CreateIndex(
                "IX_CheckoutHistories_LibraryAssetId",
                "CheckoutHistories",
                "LibraryAssetId");

            migrationBuilder.CreateIndex(
                "IX_CheckoutHistories_LibraryCardId",
                "CheckoutHistories",
                "LibraryCardId");

            migrationBuilder.CreateIndex(
                "IX_Holds_LibraryAssetId",
                "Holds",
                "LibraryAssetId");

            migrationBuilder.CreateIndex(
                "IX_Holds_LibraryCardId",
                "Holds",
                "LibraryCardId");

            migrationBuilder.CreateIndex(
                "IX_LibraryAssets_LocationId",
                "LibraryAssets",
                "LocationId");

            migrationBuilder.CreateIndex(
                "IX_LibraryAssets_StatusId",
                "LibraryAssets",
                "StatusId");

            migrationBuilder.AddForeignKey(
                "FK_Patrons_LibraryBranches_HomeLibraryBranchId",
                "Patrons",
                "HomeLibraryBranchId",
                "LibraryBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Patrons_LibraryCards_LibraryCardId",
                "Patrons",
                "LibraryCardId",
                "LibraryCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Patrons_LibraryBranches_HomeLibraryBranchId",
                "Patrons");

            migrationBuilder.DropForeignKey(
                "FK_Patrons_LibraryCards_LibraryCardId",
                "Patrons");

            migrationBuilder.DropTable(
                "BranchHours");

            migrationBuilder.DropTable(
                "Checkouts");

            migrationBuilder.DropTable(
                "CheckoutHistories");

            migrationBuilder.DropTable(
                "Holds");

            migrationBuilder.DropTable(
                "LibraryAssets");

            migrationBuilder.DropTable(
                "LibraryCards");

            migrationBuilder.DropTable(
                "LibraryBranches");

            migrationBuilder.DropTable(
                "Statuses");

            migrationBuilder.DropIndex(
                "IX_Patrons_HomeLibraryBranchId",
                "Patrons");

            migrationBuilder.DropIndex(
                "IX_Patrons_LibraryCardId",
                "Patrons");

            migrationBuilder.DropColumn(
                "HomeLibraryBranchId",
                "Patrons");

            migrationBuilder.DropColumn(
                "LibraryCardId",
                "Patrons");
        }
    }
}