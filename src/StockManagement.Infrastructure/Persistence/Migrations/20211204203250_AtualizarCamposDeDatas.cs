using Microsoft.EntityFrameworkCore.Migrations;

namespace StockManagement.Infrastructure.Migrations
{
    public partial class AtualizarCamposDeDatas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataVenda",
                table: "Vendas",
                newName: "DataCadastro");

            migrationBuilder.RenameColumn(
                name: "DataEntrada",
                table: "Estoques",
                newName: "DataCadastro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "Vendas",
                newName: "DataVenda");

            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "Estoques",
                newName: "DataEntrada");
        }
    }
}
