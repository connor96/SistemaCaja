using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CajaSistema.Data.Migrations
{
    /// <inheritdoc />
    public partial class nomodificationrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "observacion",
                schema: "CajaWeb",
                table: "NetCoreRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "observacion",
                schema: "CajaWeb",
                table: "NetCoreRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
