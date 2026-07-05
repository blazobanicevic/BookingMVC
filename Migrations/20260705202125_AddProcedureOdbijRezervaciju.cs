using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddProcedureOdbijRezervaciju : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.sp_OdbijRezervaciju
                    @IdRezervacija INT
                AS
                BEGIN
                    UPDATE dbo.Rezervacije
                    SET IdStatus = 3
                    WHERE IdRezervacija = @IdRezervacija;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE IF EXISTS dbo.sp_OdbijRezervaciju;
            ");
        }
    }
}
