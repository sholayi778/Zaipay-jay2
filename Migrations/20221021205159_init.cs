using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zaipay.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BearerToken = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    MaintainedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionRefNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SourceCurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeneficiaryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopRateUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZaiAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VirtualAccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopRateUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZaiWalletAccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZaiAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayIdReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BPayReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZaiAccounts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "TransactionRecords");

            migrationBuilder.DropTable(
                name: "ZaiAccounts");
        }
    }
}
