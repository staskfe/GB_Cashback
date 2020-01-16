using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boticatio.Cashback.Infraestrutura.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cashback");

            migrationBuilder.CreateTable(
                name: "Revendedores",
                schema: "Cashback",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 30, nullable: true),
                    CPF = table.Column<string>(maxLength: 30, nullable: true),
                    Senha = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                schema: "Cashback",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(nullable: false),
                    Valor = table.Column<float>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    RevendedorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compra_Revendedores_RevendedorId",
                        column: x => x.RevendedorId,
                        principalSchema: "Cashback",
                        principalTable: "Revendedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compra_RevendedorId",
                schema: "Cashback",
                table: "Compra",
                column: "RevendedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compra",
                schema: "Cashback");

            migrationBuilder.DropTable(
                name: "Revendedores",
                schema: "Cashback");
        }
    }
}
