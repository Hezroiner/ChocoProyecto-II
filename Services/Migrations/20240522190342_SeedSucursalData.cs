using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Services.Migrations
{
    /// <inheritdoc />
    public partial class SeedSucursalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sucursales",
                columns: new[] { "Id", "NombreSucursal" },
                values: new object[,]
                {
                    { 1, "Clinica Santa Cruz" },
                    { 2, "Clinica Nicoya" },
                    { 3, "Clinica Libera" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
