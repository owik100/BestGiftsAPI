using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BestGiftsAPI.Migrations
{
    public partial class ImagContnt_And_ExternalUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalUrl",
                table: "GiftIdeas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageContent",
                table: "GiftIdeas",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalUrl",
                table: "GiftIdeas");

            migrationBuilder.DropColumn(
                name: "ImageContent",
                table: "GiftIdeas");
        }
    }
}
