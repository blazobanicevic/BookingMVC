using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddInsteadOfUpdateSmjestajAktivanTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TRIGGER dbo.trg_InsteadOfUpdateSmjestajAktivan
                ON dbo.Smjestaji
                INSTEAD OF UPDATE
                AS
                BEGIN
                    IF EXISTS (
                        SELECT 1
                        FROM inserted i
                        INNER JOIN deleted d
                            ON i.IdSmjestaj = d.IdSmjestaj
                        INNER JOIN dbo.Rezervacije r
                            ON r.IdSmjestaj = i.IdSmjestaj
                        WHERE d.Aktivan = 1
                          AND i.Aktivan = 0
                          AND (r.IdStatus = 1 OR r.IdStatus = 2)
                          AND r.DatumOdjave > CAST(GETDATE() AS date)
                    )
                    BEGIN
                        RAISERROR(
                            'Smjestaj nije moguce deaktivirati jer ima rezervacije koje nisu zavrsene ili cekaju odluku.',
                            16,
                            1
                        );

                        RETURN;
                    END

                    UPDATE s
                    SET
                        s.IdGrad = i.IdGrad,
                        s.IdAdmin = i.IdAdmin,
                        s.IdTip = i.IdTip,
                        s.Naziv = i.Naziv,
                        s.Opis = i.Opis,
                        s.Adresa = i.Adresa,
                        s.BrojOsoba = i.BrojOsoba,
                        s.CijenaPoNoci = i.CijenaPoNoci,
                        s.Aktivan = i.Aktivan
                    FROM dbo.Smjestaji s
                    INNER JOIN inserted i
                        ON s.IdSmjestaj = i.IdSmjestaj;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS dbo.trg_InsteadOfUpdateSmjestajAktivan;
            ");
        }
    }
}
