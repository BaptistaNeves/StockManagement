using Microsoft.EntityFrameworkCore.Migrations;

namespace StockManagement.Infrastructure.Migrations
{
    public partial class PropriedadeStatusAdicionadoNaVenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Vendas",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques",
                column: "ProdutoId",
                unique: true,
                filter: "[ProdutoId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vendas");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques",
                column: "ProdutoId");
        }
    }
}
