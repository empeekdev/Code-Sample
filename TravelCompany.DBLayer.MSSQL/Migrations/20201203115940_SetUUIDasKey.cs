using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelCompany.DBLayer.MSSQL.Migrations
{
    public partial class SetUUIDasKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agent_Agency_AgencyId",
                table: "Agent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agency",
                table: "Agency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                table: "Agent");

            migrationBuilder.DropIndex(
                name: "IX_Agent_AgencyId",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "AgencyId",
                table: "Agent");

            migrationBuilder.AddColumn<Guid>(
                name: "AgencyUUID",
                table: "Agent",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agency",
                table: "Agency",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                table: "Agent",
                column: "UUID");

            migrationBuilder.CreateIndex(
                name: "IX_Agent_AgencyUUID",
                table: "Agent",
                column: "AgencyUUID");

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_Agency_AgencyUUID",
                table: "Agent",
                column: "AgencyUUID",
                principalTable: "Agency",
                principalColumn: "UUID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agent_Agency_AgencyUUID",
                table: "Agent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agency",
                table: "Agency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                table: "Agent");

            migrationBuilder.DropIndex(
                name: "IX_Agent_AgencyUUID",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "AgencyUUID",
                table: "Agent");

            migrationBuilder.AddColumn<long>(
                name: "AgencyId",
                table: "Agent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agency",
                table: "Agency",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                table: "Agent",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Agent_AgencyId",
                table: "Agent",
                column: "AgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_Agency_AgencyId",
                table: "Agent",
                column: "AgencyId",
                principalTable: "Agency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
