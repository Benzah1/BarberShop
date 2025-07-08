using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LinkTurnoWithService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Service",
                table: "Turnos");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Turnos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_ServiceId",
                table: "Turnos",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Services_ServiceId",
                table: "Turnos",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Services_ServiceId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_ServiceId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Turnos");

            migrationBuilder.AddColumn<string>(
                name: "Service",
                table: "Turnos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
