using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBarberRelationToTurno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barber",
                table: "Turnos");

            migrationBuilder.AddColumn<int>(
                name: "BarberId",
                table: "Turnos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_BarberId",
                table: "Turnos",
                column: "BarberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Barbers_BarberId",
                table: "Turnos",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Barbers_BarberId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_BarberId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "BarberId",
                table: "Turnos");

            migrationBuilder.AddColumn<string>(
                name: "Barber",
                table: "Turnos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
