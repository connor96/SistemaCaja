using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CajaSistema.Data.Migrations
{
    /// <inheritdoc />
    public partial class modificationuseridentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dni",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                newName: "rol");

            migrationBuilder.AddColumn<string>(
                name: "confirmPassword",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "password",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "confirmPassword",
                schema: "CajaWeb",
                table: "NetCoreUsers");

            migrationBuilder.DropColumn(
                name: "password",
                schema: "CajaWeb",
                table: "NetCoreUsers");

            migrationBuilder.RenameColumn(
                name: "rol",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                newName: "dni");
        }
    }
}
