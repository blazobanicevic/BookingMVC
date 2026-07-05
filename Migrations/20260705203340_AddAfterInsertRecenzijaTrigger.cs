using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddAfterInsertRecenzijaTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TRIGGER dbo.trg_AfterInsertRecenzija
                ON dbo.Recenzije
                AFTER INSERT
                AS
                BEGIN
                    INSERT INTO dbo.Notifikacije (IdKorisnik, Naslov, Tekst)
                    SELECT
                        s.IdAdmin,
                        'Nova recenzija',
                        'Korisnik je ostavio novu recenziju za vas smjestaj ""' + s.Naziv + '"".'
                    FROM inserted i
                    INNER JOIN dbo.Smjestaji s
                        ON i.IdSmjestaj = s.IdSmjestaj;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS dbo.trg_AfterInsertRecenzija;
            ");
        }
    }
}
