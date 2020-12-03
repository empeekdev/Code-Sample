using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TravelCompany.DBLayer.PostgreSQL.Migrations
{
    public partial class SetUUIDasKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelAgency",
                table: "TravelAgency");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TravelAgency",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UUID",
                table: "TravelAgency",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelAgency",
                table: "TravelAgency",
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
                    TravelAgencyUUID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_Agent_TravelAgency_TravelAgencyUUID",
                        column: x => x.TravelAgencyUUID,
                        principalTable: "TravelAgency",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agent_TravelAgencyUUID",
                table: "Agent",
                column: "TravelAgencyUUID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelAgency",
                table: "TravelAgency");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "TravelAgency");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TravelAgency",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelAgency",
                table: "TravelAgency",
                column: "Id");
        }
    }
}
