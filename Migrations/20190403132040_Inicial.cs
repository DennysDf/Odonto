using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Odonto.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anamneses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: false),
                    DataCad = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())"),
                    IsAtivo = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anamneses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consultorios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Endereco = table.Column<string>(nullable: true),
                    IsAtivo = table.Column<bool>(nullable: false, defaultValue: true),
                    DataCad = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultorios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicamentos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    principio_ativo = table.Column<string>(nullable: true),
                    laboratorio = table.Column<string>(nullable: true),
                    nome = table.Column<string>(nullable: true),
                    apresentacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamentos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ResetSenha",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    DataHora = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetSenha", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Extensao = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: false),
                    ConsultorioId = table.Column<int>(nullable: false),
                    IsAtivo = table.Column<bool>(nullable: false, defaultValue: true),
                    LastAcesso = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Consultorios_ConsultorioId",
                        column: x => x.ConsultorioId,
                        principalTable: "Consultorios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    DataNasc = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Endereco = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EstadoCivil = table.Column<string>(nullable: true),
                    Convenio = table.Column<string>(nullable: true),
                    NumConvenio = table.Column<string>(nullable: true),
                    Profissao = table.Column<string>(nullable: true),
                    InicioTrate = table.Column<string>(nullable: true),
                    Obs = table.Column<string>(nullable: true),
                    DentistaOrigem = table.Column<string>(nullable: true),
                    Extensao = table.Column<string>(nullable: true),
                    DataCad = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())"),
                    IsAtivo = table.Column<bool>(nullable: false, defaultValue: true),
                    IdResp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_IdResp",
                        column: x => x.IdResp,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClienteAnamneses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClienteId = table.Column<int>(nullable: false),
                    AnamneseId = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    DataCad = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())"),
                    IsAtivo = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteAnamneses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteAnamneses_Anamneses_AnamneseId",
                        column: x => x.AnamneseId,
                        principalTable: "Anamneses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClienteAnamneses_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Extensao = table.Column<string>(nullable: true),
                    DataRealizao = table.Column<string>(nullable: true),
                    DataCad = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())"),
                    ClienteId = table.Column<int>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    IsAtivo = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exames_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Procedimentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    DataRealizacao = table.Column<string>(nullable: true),
                    DataCad = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())"),
                    ClienteId = table.Column<int>(nullable: false),
                    IsAtivo = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedimentos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receituarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClienteId = table.Column<int>(nullable: false),
                    ClientesId = table.Column<int>(nullable: true),
                    DataCad = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receituarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receituarios_Clientes_ClientesId",
                        column: x => x.ClientesId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receituario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Medicamento = table.Column<int>(nullable: false),
                    MedicamentoId = table.Column<int>(nullable: true),
                    RecedituarioId = table.Column<int>(nullable: false),
                    Concentracao = table.Column<string>(nullable: true),
                    Forma = table.Column<int>(nullable: false),
                    Dose = table.Column<string>(nullable: true),
                    AdmVia = table.Column<int>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    Duracao = table.Column<int>(nullable: false),
                    DuracaoEm = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receituario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receituario_Medicamentos_MedicamentoId",
                        column: x => x.MedicamentoId,
                        principalTable: "Medicamentos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receituario_Receituarios_RecedituarioId",
                        column: x => x.RecedituarioId,
                        principalTable: "Receituarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteAnamneses_AnamneseId",
                table: "ClienteAnamneses",
                column: "AnamneseId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteAnamneses_ClienteId",
                table: "ClienteAnamneses",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IdResp",
                table: "Clientes",
                column: "IdResp");

            migrationBuilder.CreateIndex(
                name: "IX_Exames_ClienteId",
                table: "Exames",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedimentos_ClienteId",
                table: "Procedimentos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Receituario_MedicamentoId",
                table: "Receituario",
                column: "MedicamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Receituario_RecedituarioId",
                table: "Receituario",
                column: "RecedituarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Receituarios_ClientesId",
                table: "Receituarios",
                column: "ClientesId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ConsultorioId",
                table: "Usuarios",
                column: "ConsultorioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteAnamneses");

            migrationBuilder.DropTable(
                name: "Exames");

            migrationBuilder.DropTable(
                name: "Procedimentos");

            migrationBuilder.DropTable(
                name: "Receituario");

            migrationBuilder.DropTable(
                name: "ResetSenha");

            migrationBuilder.DropTable(
                name: "Anamneses");

            migrationBuilder.DropTable(
                name: "Medicamentos");

            migrationBuilder.DropTable(
                name: "Receituarios");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Consultorios");
        }
    }
}
