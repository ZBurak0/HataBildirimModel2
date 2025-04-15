using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HataBildirimModel2.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFaulNotoficationForModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VerificationCodeExpiry",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FaultNotifications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "FaultNotifications");

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationCodeExpiry",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }
    }
}
