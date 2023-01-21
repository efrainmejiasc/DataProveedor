using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModeloDataProveedor.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rol = table.Column<string>(type: "VARCHAR(50)", nullable: false, defaultValue: "True")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Rol);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Apellido = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    UserName = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    Password2 = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Estado = table.Column<bool>(type: "BIT", nullable: false),
                    RolId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Email);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                column: "Rol",
                value: "Administrador");

            migrationBuilder.InsertData(
                table: "Roles",
                column: "Rol",
                value: "Usuario");

            migrationBuilder.InsertData(
                table: "Roles",
                column: "Rol",
                value: "Proveedor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
