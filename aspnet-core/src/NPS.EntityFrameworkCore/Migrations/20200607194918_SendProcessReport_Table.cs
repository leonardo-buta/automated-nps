using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NPS.Migrations
{
    public partial class SendProcessReport_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "send_process_reports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Recipient = table.Column<string>(nullable: false),
                    SendProcessId = table.Column<int>(nullable: true),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_send_process_reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_send_process_reports_send_processes_SendProcessId",
                        column: x => x.SendProcessId,
                        principalTable: "send_processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_send_process_reports_SendProcessId",
                table: "send_process_reports",
                column: "SendProcessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "send_process_reports");
        }
    }
}
