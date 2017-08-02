using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelBlog.Migrations
{
    public partial class AddPeoplePropertyToLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "People",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_LocationId",
                table: "People",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Locations_LocationId",
                table: "People",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Locations_LocationId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_LocationId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "People");
        }
    }
}
