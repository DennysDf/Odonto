using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Odonto.Migrations
{
    public partial class Inicial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receituario_Medicamentos_MedicamentoId",
                table: "Receituario");

            migrationBuilder.DropForeignKey(
                name: "FK_Receituario_Receituarios_RecedituarioId",
                table: "Receituario");

            migrationBuilder.DropForeignKey(
                name: "FK_Receituarios_Clientes_ClientesId",
                table: "Receituarios");

            migrationBuilder.DropIndex(
                name: "IX_Receituarios_ClientesId",
                table: "Receituarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receituario",
                table: "Receituario");

            migrationBuilder.DropColumn(
                name: "ClientesId",
                table: "Receituarios");

            migrationBuilder.RenameTable(
                name: "Receituario",
                newName: "ReceituarioMedicamentos");

            migrationBuilder.RenameIndex(
                name: "IX_Receituario_RecedituarioId",
                table: "ReceituarioMedicamentos",
                newName: "IX_ReceituarioMedicamentos_RecedituarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Receituario_MedicamentoId",
                table: "ReceituarioMedicamentos",
                newName: "IX_ReceituarioMedicamentos_MedicamentoId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCad",
                table: "Receituarios",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Medicamento",
                table: "ReceituarioMedicamentos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceituarioMedicamentos",
                table: "ReceituarioMedicamentos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Receituarios_ClienteId",
                table: "Receituarios",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceituarioMedicamentos_Medicamentos_MedicamentoId",
                table: "ReceituarioMedicamentos",
                column: "MedicamentoId",
                principalTable: "Medicamentos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceituarioMedicamentos_Receituarios_RecedituarioId",
                table: "ReceituarioMedicamentos",
                column: "RecedituarioId",
                principalTable: "Receituarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receituarios_Clientes_ClienteId",
                table: "Receituarios",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceituarioMedicamentos_Medicamentos_MedicamentoId",
                table: "ReceituarioMedicamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceituarioMedicamentos_Receituarios_RecedituarioId",
                table: "ReceituarioMedicamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Receituarios_Clientes_ClienteId",
                table: "Receituarios");

            migrationBuilder.DropIndex(
                name: "IX_Receituarios_ClienteId",
                table: "Receituarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceituarioMedicamentos",
                table: "ReceituarioMedicamentos");

            migrationBuilder.RenameTable(
                name: "ReceituarioMedicamentos",
                newName: "Receituario");

            migrationBuilder.RenameIndex(
                name: "IX_ReceituarioMedicamentos_RecedituarioId",
                table: "Receituario",
                newName: "IX_Receituario_RecedituarioId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceituarioMedicamentos_MedicamentoId",
                table: "Receituario",
                newName: "IX_Receituario_MedicamentoId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCad",
                table: "Receituarios",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<int>(
                name: "ClientesId",
                table: "Receituarios",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Medicamento",
                table: "Receituario",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receituario",
                table: "Receituario",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Receituarios_ClientesId",
                table: "Receituarios",
                column: "ClientesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receituario_Medicamentos_MedicamentoId",
                table: "Receituario",
                column: "MedicamentoId",
                principalTable: "Medicamentos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receituario_Receituarios_RecedituarioId",
                table: "Receituario",
                column: "RecedituarioId",
                principalTable: "Receituarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receituarios_Clientes_ClientesId",
                table: "Receituarios",
                column: "ClientesId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
