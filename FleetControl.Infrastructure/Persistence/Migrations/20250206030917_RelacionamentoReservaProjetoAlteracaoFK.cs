using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetControl.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoReservaProjetoAlteracaoFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Projects_ProjectId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ProjectId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IdProject",
                table: "Reservations",
                column: "IdProject");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Projects_IdProject",
                table: "Reservations",
                column: "IdProject",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Projects_IdProject",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_IdProject",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ProjectId",
                table: "Reservations",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Projects_ProjectId",
                table: "Reservations",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
