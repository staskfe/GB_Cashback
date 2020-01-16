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
                    table.PrimaryKey("PK_Revendedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                schema: "Cashback",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(maxLength: 100, nullable: false),
                    Valor = table.Column<float>(maxLength: 30, nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Revendedor_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_Revendedores_Revendedor_Id",
                        column: x => x.Revendedor_Id,
                        principalSchema: "Cashback",
                        principalTable: "Revendedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_Revendedor_Id",
                schema: "Cashback",
                table: "Compras",
                column: "Revendedor_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compras",
                schema: "Cashback");

            migrationBuilder.DropTable(
                name: "Revendedores",
                schema: "Cashback");
        }
    }
}
