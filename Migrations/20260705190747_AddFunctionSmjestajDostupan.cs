using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddFunctionSmjestajDostupan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE FUNCTION dbo.fn_SmjestajDostupan
            (
                @IdSmjestaj INT,
                @DatumPrijave DATE,
                @DatumOdjave DATE
            )
            RETURNS BIT
            AS
            BEGIN
                DECLARE @Dostupan BIT = 1;

                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.Rezervacije
                    WHERE IdSmjestaj = @IdSmjestaj
                      AND (IdStatus = 1 OR IdStatus = 2)
                      AND @DatumPrijave < DatumOdjave
                      AND @DatumOdjave > DatumPrijave
                )
                BEGIN
                    SET @Dostupan = 0;
                END

                RETURN @Dostupan;
            END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DROP FUNCTION IF EXISTS dbo.fn_SmjestajDostupan;
            ");
        }
    }
}
