using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingMVC.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "IdKorisnik", "Email", "IdUloga", "Ime", "Lozinka", "Prezime" },
                values: new object[] { 1, "admin@bookingmvc.com", 2, "Admin", "admin123", "Booking" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "IdKorisnik",
                keyValue: 1);
        }
    }
}
