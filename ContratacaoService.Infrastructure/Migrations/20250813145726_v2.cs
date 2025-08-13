using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContratacaoService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Propostas",
                table: "Propostas");

            migrationBuilder.RenameTable(
                name: "Propostas",
                newName: "PropostasSnapShot");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropostasSnapShot",
                table: "PropostasSnapShot",
                column: "PropostaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PropostasSnapShot",
                table: "PropostasSnapShot");

            migrationBuilder.RenameTable(
                name: "PropostasSnapShot",
                newName: "Propostas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Propostas",
                table: "Propostas",
                column: "PropostaId");
        }
    }
}
