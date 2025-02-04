using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChuckIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixingListingAndMessagesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Listings_ListingId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ListingId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ListingId1",
                table: "Messages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ListingId1",
                table: "Messages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ListingId1",
                table: "Messages",
                column: "ListingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Listings_ListingId1",
                table: "Messages",
                column: "ListingId1",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
