using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CajaSistema.Data.Migrations
{
    /// <inheritdoc />
    public partial class identitychanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NetCoreRoleClaims_NetCoreRoles_RoleId",
                schema: "CajaWeb",
                table: "NetCoreRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_NetCoreUserClaims_NetCoreUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_NetCoreUserLogins_NetCoreUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_NetCoreUserRoles_NetCoreRoles_RoleId",
                schema: "CajaWeb",
                table: "NetCoreUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_NetCoreUserRoles_NetCoreUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_NetCoreUserTokens_NetCoreUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserTokens");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                schema: "CajaWeb",
                table: "NetCoreUsers");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                schema: "CajaWeb",
                table: "NetCoreUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                schema: "CajaWeb",
                table: "NetCoreRoles");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                schema: "CajaWeb",
                table: "NetCoreRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "CajaWeb",
                table: "NetCoreRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    aPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombres1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombres2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idPersona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_NetCoreRoleClaims_AspNetRoles_RoleId",
                schema: "CajaWeb",
                table: "NetCoreRoleClaims",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NetCoreUserClaims_AspNetUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserClaims",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NetCoreUserLogins_AspNetUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserLogins",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NetCoreUserRoles_AspNetRoles_RoleId",
                schema: "CajaWeb",
                table: "NetCoreUserRoles",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NetCoreUserRoles_AspNetUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserRoles",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NetCoreUserTokens_AspNetUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserTokens",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NetCoreRoleClaims_AspNetRoles_RoleId",
                schema: "CajaWeb",
                table: "NetCoreRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_NetCoreUserClaims_AspNetUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_NetCoreUserLogins_AspNetUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_NetCoreUserRoles_AspNetRoles_RoleId",
                schema: "CajaWeb",
                table: "NetCoreUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_NetCoreUserRoles_AspNetUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_NetCoreUserTokens_AspNetUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "dbo");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                schema: "CajaWeb",
                table: "NetCoreRoles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "CajaWeb",
                table: "NetCoreRoles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "CajaWeb",
                table: "NetCoreRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_NetCoreRoleClaims_NetCoreRoles_RoleId",
                schema: "CajaWeb",
                table: "NetCoreRoleClaims",
                column: "RoleId",
                principalSchema: "CajaWeb",
                principalTable: "NetCoreRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NetCoreUserClaims_NetCoreUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserClaims",
                column: "UserId",
                principalSchema: "CajaWeb",
                principalTable: "NetCoreUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NetCoreUserLogins_NetCoreUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserLogins",
                column: "UserId",
                principalSchema: "CajaWeb",
                principalTable: "NetCoreUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NetCoreUserRoles_NetCoreRoles_RoleId",
                schema: "CajaWeb",
                table: "NetCoreUserRoles",
                column: "RoleId",
                principalSchema: "CajaWeb",
                principalTable: "NetCoreRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NetCoreUserRoles_NetCoreUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserRoles",
                column: "UserId",
                principalSchema: "CajaWeb",
                principalTable: "NetCoreUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NetCoreUserTokens_NetCoreUsers_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserTokens",
                column: "UserId",
                principalSchema: "CajaWeb",
                principalTable: "NetCoreUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
