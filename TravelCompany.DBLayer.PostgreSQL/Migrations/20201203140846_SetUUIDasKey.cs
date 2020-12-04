using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace TravelCompany.DBLayer.PostgreSQL.Migrations
{
    public partial class SetUUIDasKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Agency",
                table: "Agency");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Agency",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UUID",
                table: "Agency",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agency",
                table: "Agency",
                column: "UUID");

            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AgencyUUID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_Agent_Agency_AgencyUUID",
                        column: x => x.AgencyUUID,
                        principalTable: "Agency",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agent_AgencyUUID",
                table: "Agent",
                column: "AgencyUUID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agency",
                table: "Agency");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "Agency");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Agency",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agency",
                table: "Agency",
                column: "Id");
        }
    }
}
