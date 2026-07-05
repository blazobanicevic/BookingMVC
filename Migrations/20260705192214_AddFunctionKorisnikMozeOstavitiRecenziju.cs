using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddFunctionKorisnikMozeOstavitiRecenziju : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE FUNCTION dbo.fn_KorisnikMozeOstavitiRecenziju
            (
                @IdKorisnik INT,
                @IdSmjestaj INT
            )
            RETURNS BIT
            AS
            BEGIN
                DECLARE @Moze BIT = 0;

                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.Rezervacije
                    WHERE IdKorisnik = @IdKorisnik
                      AND IdSmjestaj = @IdSmjestaj
                      AND IdStatus = 2
                      AND DatumOdjave < CAST(GETDATE() AS date)
                )
                AND NOT EXISTS
                (
                    SELECT 1
                    FROM dbo.Recenzije
                    WHERE IdKorisnik = @IdKorisnik
                      AND IdSmjestaj = @IdSmjestaj
                )
                BEGIN
                    SET @Moze = 1;
                END

                RETURN @Moze;
            END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DROP FUNCTION IF EXISTS dbo.fn_KorisnikMozeOstavitiRecenziju;
            ");
        }
    }
}
