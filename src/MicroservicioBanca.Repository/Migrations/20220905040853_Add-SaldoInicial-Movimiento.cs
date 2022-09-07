using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroservicioBanca.Migrations
{
    public partial class AddSaldoInicialMovimiento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "SaldoInicial",
                schema: "banca",
                table: "Movimiento",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaldoInicial",
                schema: "banca",
                table: "Movimiento");
        }
    }
}
