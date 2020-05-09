using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NPS.Migrations
{
    public partial class Message_types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AbpOrganizationUnits_TenantId_Code",
                table: "AbpOrganizationUnits");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "mensagens");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "mensagens");

            migrationBuilder.DropColumn(
                name: "Texto",
                table: "mensagens");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "mensagens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "mensagens",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "mensagens",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "mensagens",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MessageTypeId",
                table: "mensagens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "mensagens",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "mensagens",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "message_types",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message_types", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "message_types",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, "E-Mail" });

            migrationBuilder.InsertData(
                table: "message_types",
                columns: new[] { "Id", "Type" },
                values: new object[] { 2, "SMS" });

            migrationBuilder.CreateIndex(
                name: "IX_mensagens_MessageTypeId",
                table: "mensagens",
                column: "MessageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnits_TenantId_Code",
                table: "AbpOrganizationUnits",
                columns: new[] { "TenantId", "Code" });

            migrationBuilder.AddForeignKey(
                name: "FK_mensagens_message_types_MessageTypeId",
                table: "mensagens",
                column: "MessageTypeId",
                principalTable: "message_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mensagens_message_types_MessageTypeId",
                table: "mensagens");

            migrationBuilder.DropTable(
                name: "message_types");

            migrationBuilder.DropIndex(
                name: "IX_mensagens_MessageTypeId",
                table: "mensagens");

            migrationBuilder.DropIndex(
                name: "IX_AbpOrganizationUnits_TenantId_Code",
                table: "AbpOrganizationUnits");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "mensagens");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "mensagens");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "mensagens");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "mensagens");

            migrationBuilder.DropColumn(
                name: "MessageTypeId",
                table: "mensagens");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "mensagens");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "mensagens");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "mensagens",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "mensagens",
                type: "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Texto",
                table: "mensagens",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnits_TenantId_Code",
                table: "AbpOrganizationUnits",
                columns: new[] { "TenantId", "Code" },
                unique: true);
        }
    }
}
