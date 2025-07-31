using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroEmpresa.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoCompletoPorComplemento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Completo",
                table: "Empresas",
                newName: "Complemento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Complemento",
                table: "Empresas",
                newName: "Completo");
        }
    }
}
