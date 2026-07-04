using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingMVC.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Gradovi",
                columns: new[] { "IdGrad", "Drzava", "Naziv" },
                values: new object[,]
                {
                    { 1, "Crna Gora", "Podgorica" },
                    { 2, "Crna Gora", "Budva" },
                    { 3, "Crna Gora", "Kotor" },
                    { 4, "Crna Gora", "Herceg Novi" },
                    { 5, "Crna Gora", "Bar" }
                });

            migrationBuilder.InsertData(
                table: "Sadrzaji",
                columns: new[] { "IdSadrzaj", "Naziv" },
                values: new object[,]
                {
                    { 1, "Wi-Fi" },
                    { 2, "Parking" },
                    { 3, "Klima" },
                    { 4, "Kuhinja" },
                    { 5, "Terasa" },
                    { 6, "Bazen" }
                });

            migrationBuilder.InsertData(
                table: "StatusiRezervacija",
                columns: new[] { "IdStatus", "Naziv" },
                values: new object[,]
                {
                    { 1, "Na cekanju" },
                    { 2, "Potvrdjena" },
                    { 3, "Odbijena" },
                    { 4, "Otkazana" }
                });

            migrationBuilder.InsertData(
                table: "TipoviSmjestaja",
                columns: new[] { "IdTipSmjestaja", "Naziv" },
                values: new object[,]
                {
                    { 1, "Apartman" },
                    { 2, "Studio" },
                    { 3, "Soba" },
                    { 4, "Kuca" }
                });

            migrationBuilder.InsertData(
                table: "Uloge",
                columns: new[] { "IdUloga", "Naziv" },
                values: new object[,]
                {
                    { 1, "Korisnik" },
                    { 2, "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "IdGrad",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "IdGrad",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "IdGrad",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "IdGrad",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Gradovi",
                keyColumn: "IdGrad",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sadrzaji",
                keyColumn: "IdSadrzaj",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sadrzaji",
                keyColumn: "IdSadrzaj",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sadrzaji",
                keyColumn: "IdSadrzaj",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sadrzaji",
                keyColumn: "IdSadrzaj",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sadrzaji",
                keyColumn: "IdSadrzaj",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sadrzaji",
                keyColumn: "IdSadrzaj",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "StatusiRezervacija",
                keyColumn: "IdStatus",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StatusiRezervacija",
                keyColumn: "IdStatus",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StatusiRezervacija",
                keyColumn: "IdStatus",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StatusiRezervacija",
                keyColumn: "IdStatus",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TipoviSmjestaja",
                keyColumn: "IdTipSmjestaja",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoviSmjestaja",
                keyColumn: "IdTipSmjestaja",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TipoviSmjestaja",
                keyColumn: "IdTipSmjestaja",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TipoviSmjestaja",
                keyColumn: "IdTipSmjestaja",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Uloge",
                keyColumn: "IdUloga",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Uloge",
                keyColumn: "IdUloga",
                keyValue: 2);
        }
    }
}
