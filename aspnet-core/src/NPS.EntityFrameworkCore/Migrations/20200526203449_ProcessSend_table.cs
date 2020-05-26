using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NPS.Migrations
{
    public partial class ProcessSend_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "send_processes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Separator = table.Column<string>(maxLength: 1, nullable: false),
                    CampaignId = table.Column<int>(nullable: true),
                    MessageId = table.Column<int>(nullable: true),
                    SendDate = table.Column<DateTime>(nullable: false),
                    UploadedMailing = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_send_processes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_send_processes_campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_send_processes_messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mailings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProcessSendId = table.Column<int>(nullable: true),
                    Line = table.Column<string>(maxLength: 500, nullable: false),
                    Valid = table.Column<bool>(nullable: false, defaultValue: false),
                    Duplicated = table.Column<bool>(nullable: false, defaultValue: false),
                    Empty = table.Column<bool>(nullable: false, defaultValue: false),
                    IncorretFormat = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mailings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mailings_send_processes_ProcessSendId",
                        column: x => x.ProcessSendId,
                        principalTable: "send_processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mailings_ProcessSendId",
                table: "mailings",
                column: "ProcessSendId");

            migrationBuilder.CreateIndex(
                name: "IX_send_processes_CampaignId",
                table: "send_processes",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_send_processes_MessageId",
                table: "send_processes",
                column: "MessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mailings");

            migrationBuilder.DropTable(
                name: "send_processes");
        }
    }
}
