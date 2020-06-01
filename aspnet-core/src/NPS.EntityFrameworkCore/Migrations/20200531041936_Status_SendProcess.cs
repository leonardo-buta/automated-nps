using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NPS.Migrations
{
    public partial class Status_SendProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_send_processes_campaigns_CampaignId",
                table: "send_processes");

            migrationBuilder.DropIndex(
                name: "IX_send_processes_CampaignId",
                table: "send_processes");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "send_processes");

            migrationBuilder.DropColumn(
                name: "SendDate",
                table: "send_processes");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduleDate",
                table: "send_processes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StatusSendProcess",
                table: "send_processes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleDate",
                table: "send_processes");

            migrationBuilder.DropColumn(
                name: "StatusSendProcess",
                table: "send_processes");

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "send_processes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SendDate",
                table: "send_processes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_send_processes_CampaignId",
                table: "send_processes",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_send_processes_campaigns_CampaignId",
                table: "send_processes",
                column: "CampaignId",
                principalTable: "campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
