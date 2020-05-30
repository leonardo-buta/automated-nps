using Microsoft.EntityFrameworkCore.Migrations;

namespace NPS.Migrations
{
    public partial class Typo_Campaign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_campaigns_CampaingId",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_CampaingId",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "CampaingId",
                table: "messages");

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "messages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_messages_CampaignId",
                table: "messages",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_campaigns_CampaignId",
                table: "messages",
                column: "CampaignId",
                principalTable: "campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_campaigns_CampaignId",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_CampaignId",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "messages");

            migrationBuilder.AddColumn<int>(
                name: "CampaingId",
                table: "messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_messages_CampaingId",
                table: "messages",
                column: "CampaingId");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_campaigns_CampaingId",
                table: "messages",
                column: "CampaingId",
                principalTable: "campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
