using Microsoft.EntityFrameworkCore.Migrations;

namespace NPS.Migrations
{
    public partial class Status_SendProcess_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "status_send_process",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20) CHARACTER SET utf8mb4",
                oldMaxLength: 20);

            migrationBuilder.UpdateData(
                table: "status_send_process",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Pendente");

            migrationBuilder.UpdateData(
                table: "status_send_process",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Aguardando agendamento");

            migrationBuilder.UpdateData(
                table: "status_send_process",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Enviando");

            migrationBuilder.UpdateData(
                table: "status_send_process",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Finalizado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "status_send_process",
                type: "varchar(20) CHARACTER SET utf8mb4",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "status_send_process",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Pending Confirmation");

            migrationBuilder.UpdateData(
                table: "status_send_process",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Awaiting Schedule");

            migrationBuilder.UpdateData(
                table: "status_send_process",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Sending");

            migrationBuilder.UpdateData(
                table: "status_send_process",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Done");
        }
    }
}
