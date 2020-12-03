using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelCompany.DBLayer.MSSQL.Migrations
{
    public partial class SetUUIDasKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agent_TravelAgency_TravelAgencyId",
                table: "Agent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelAgency",
                table: "TravelAgency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                table: "Agent");

            migrationBuilder.DropIndex(
                name: "IX_Agent_TravelAgencyId",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "TravelAgencyId",
                table: "Agent");

            migrationBuilder.AddColumn<Guid>(
                name: "TravelAgencyUUID",
                table: "Agent",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelAgency",
                table: "TravelAgency",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                table: "Agent",
                column: "UUID");

            migrationBuilder.CreateIndex(
                name: "IX_Agent_TravelAgencyUUID",
                table: "Agent",
                column: "TravelAgencyUUID");

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_TravelAgency_TravelAgencyUUID",
                table: "Agent",
                column: "TravelAgencyUUID",
                principalTable: "TravelAgency",
                principalColumn: "UUID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agent_TravelAgency_TravelAgencyUUID",
                table: "Agent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelAgency",
                table: "TravelAgency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                table: "Agent");

            migrationBuilder.DropIndex(
                name: "IX_Agent_TravelAgencyUUID",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "TravelAgencyUUID",
                table: "Agent");

            migrationBuilder.AddColumn<long>(
                name: "TravelAgencyId",
                table: "Agent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelAgency",
                table: "TravelAgency",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                table: "Agent",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Agent_TravelAgencyId",
                table: "Agent",
                column: "TravelAgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_TravelAgency_TravelAgencyId",
                table: "Agent",
                column: "TravelAgencyId",
                principalTable: "TravelAgency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
