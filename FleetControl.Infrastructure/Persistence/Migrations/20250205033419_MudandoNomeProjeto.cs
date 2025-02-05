using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetControl.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MudandoNomeProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Projects",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Projects",
                newName: "Nome");
        }
    }
}
