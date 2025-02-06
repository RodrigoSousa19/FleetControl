using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetControl.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoReservaProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Customers_IdCustomer",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "IdCustomer",
                table: "Reservations",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_IdCustomer",
                table: "Reservations",
                newName: "IX_Reservations_ProjectId");

            migrationBuilder.AddColumn<int>(
                name: "IdProject",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Projects_ProjectId",
                table: "Reservations",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Projects_ProjectId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IdProject",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Reservations",
                newName: "IdCustomer");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ProjectId",
                table: "Reservations",
                newName: "IX_Reservations_IdCustomer");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Customers_IdCustomer",
                table: "Reservations",
                column: "IdCustomer",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
