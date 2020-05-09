using Microsoft.EntityFrameworkCore.Migrations;

namespace NPS.Migrations
{
    public partial class Rename_Messages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mensagens_message_types_MessageTypeId",
                table: "mensagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_mensagens",
                table: "mensagens");

            migrationBuilder.RenameTable(
                name: "mensagens",
                newName: "messages");

            migrationBuilder.RenameIndex(
                name: "IX_mensagens_MessageTypeId",
                table: "messages",
                newName: "IX_messages_MessageTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_messages",
                table: "messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_message_types_MessageTypeId",
                table: "messages",
                column: "MessageTypeId",
                principalTable: "message_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_message_types_MessageTypeId",
                table: "messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_messages",
                table: "messages");

            migrationBuilder.RenameTable(
                name: "messages",
                newName: "mensagens");

            migrationBuilder.RenameIndex(
                name: "IX_messages_MessageTypeId",
                table: "mensagens",
                newName: "IX_mensagens_MessageTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mensagens",
                table: "mensagens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_mensagens_message_types_MessageTypeId",
                table: "mensagens",
                column: "MessageTypeId",
                principalTable: "message_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
