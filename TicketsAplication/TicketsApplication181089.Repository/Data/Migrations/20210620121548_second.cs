using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsApplication.Repository.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "TicketDescription",
                table: "Tickets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TicketImage",
                table: "Tickets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TicketName",
                table: "Tickets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TicketPrice",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketDescription",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketImage",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketName",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductPrice",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
