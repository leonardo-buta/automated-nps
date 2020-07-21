using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NPS.Migrations
{
    public partial class SendProcessReport_Table_SendDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "send_process_reports",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseDate",
                table: "send_process_reports",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SendDate",
                table: "send_process_reports",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseDate",
                table: "send_process_reports");

            migrationBuilder.DropColumn(
                name: "SendDate",
                table: "send_process_reports");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "send_process_reports",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
