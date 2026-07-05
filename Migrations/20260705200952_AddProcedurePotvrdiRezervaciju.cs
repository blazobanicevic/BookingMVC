using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddProcedurePotvrdiRezervaciju : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.sp_PotvrdiRezervaciju
                    @IdRezervacija INT
                AS
                BEGIN
                    UPDATE dbo.Rezervacije
                    SET IdStatus = 2
                    WHERE IdRezervacija = @IdRezervacija;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE IF EXISTS dbo.sp_PotvrdiRezervaciju;
            ");
        }
    }
}
