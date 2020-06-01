using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NPS.Migrations
{
    public partial class Status_SendProcess_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusSendProcess",
                table: "send_processes");

            migrationBuilder.AddColumn<int>(
                name: "StatusSendProcessId",
                table: "send_processes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "status_send_process",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status_send_process", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "status_send_process",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending Confirmation" },
                    { 2, "Awaiting Schedule" },
                    { 3, "Sending" },
                    { 4, "Done" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_send_processes_StatusSendProcessId",
                table: "send_processes",
                column: "StatusSendProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_send_processes_status_send_process_StatusSendProcessId",
                table: "send_processes",
                column: "StatusSendProcessId",
                principalTable: "status_send_process",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_send_processes_status_send_process_StatusSendProcessId",
                table: "send_processes");

            migrationBuilder.DropTable(
                name: "status_send_process");

            migrationBuilder.DropIndex(
                name: "IX_send_processes_StatusSendProcessId",
                table: "send_processes");

            migrationBuilder.DropColumn(
                name: "StatusSendProcessId",
                table: "send_processes");

            migrationBuilder.AddColumn<int>(
                name: "StatusSendProcess",
                table: "send_processes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
