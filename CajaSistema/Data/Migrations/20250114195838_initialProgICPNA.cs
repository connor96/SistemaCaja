using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CajaSistema.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialProgICPNA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.EnsureSchema(
                name: "CajaWeb");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "NetCoreUserTokens",
                newSchema: "CajaWeb");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "NetCoreUsers",
                newSchema: "CajaWeb");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "NetCoreUserRoles",
                newSchema: "CajaWeb");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "NetCoreUserLogins",
                newSchema: "CajaWeb");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "NetCoreUserClaims",
                newSchema: "CajaWeb");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "NetCoreRoles",
                newSchema: "CajaWeb");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "NetCoreRoleClaims",
                newSchema: "CajaWeb");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "CajaWeb",
                table: "NetCoreUserRoles",
                newName: "IX_NetCoreUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserLogins",
                newName: "IX_NetCoreUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "CajaWeb",
                table: "NetCoreUserClaims",
                newName: "IX_NetCoreUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "CajaWeb",
                table: "NetCoreRoleClaims",
                newName: "IX_NetCoreRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NetCoreUserTokens",
                schema: "CajaWeb",
                table: "NetCoreUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NetCoreUsers",
                schema: "CajaWeb",
                table: "NetCoreUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NetCoreUserRoles",
                schema: "CajaWeb",
                table: "NetCoreUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NetCoreUserLogins",
                schema: "CajaWeb",
                table: "NetCoreUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NetCoreUserClaims",
                schema: "CajaWeb",
                table: "NetCoreUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NetCoreRoles",
                schema: "CajaWeb",
                table: "NetCoreRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NetCoreRoleClaims",
                schema: "CajaWeb",
                table: "NetCoreRoleClaims",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_NetCoreUserTokens",
                schema: "CajaWeb",
                table: "NetCoreUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NetCoreUsers",
                schema: "CajaWeb",
                table: "NetCoreUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NetCoreUserRoles",
                schema: "CajaWeb",
                table: "NetCoreUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NetCoreUserLogins",
                schema: "CajaWeb",
                table: "NetCoreUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NetCoreUserClaims",
                schema: "CajaWeb",
                table: "NetCoreUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NetCoreRoles",
                schema: "CajaWeb",
                table: "NetCoreRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NetCoreRoleClaims",
                schema: "CajaWeb",
                table: "NetCoreRoleClaims");

            migrationBuilder.RenameTable(
                name: "NetCoreUserTokens",
                schema: "CajaWeb",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "NetCoreUsers",
                schema: "CajaWeb",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "NetCoreUserRoles",
                schema: "CajaWeb",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "NetCoreUserLogins",
                schema: "CajaWeb",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "NetCoreUserClaims",
                schema: "CajaWeb",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "NetCoreRoles",
                schema: "CajaWeb",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "NetCoreRoleClaims",
                schema: "CajaWeb",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameIndex(
                name: "IX_NetCoreUserRoles_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_NetCoreUserLogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_NetCoreUserClaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_NetCoreRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
