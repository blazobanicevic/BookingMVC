using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    IdGrad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Drzava = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.IdGrad);
                });

            migrationBuilder.CreateTable(
                name: "Sadrzaji",
                columns: table => new
                {
                    IdSadrzaj = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sadrzaji", x => x.IdSadrzaj);
                });

            migrationBuilder.CreateTable(
                name: "StatusiRezervacija",
                columns: table => new
                {
                    IdStatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusiRezervacija", x => x.IdStatus);
                });

            migrationBuilder.CreateTable(
                name: "TipoviSmjestaja",
                columns: table => new
                {
                    IdTipSmjestaja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviSmjestaja", x => x.IdTipSmjestaja);
                });

            migrationBuilder.CreateTable(
                name: "Uloge",
                columns: table => new
                {
                    IdUloga = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloge", x => x.IdUloga);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    IdKorisnik = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUloga = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.IdKorisnik);
                    table.ForeignKey(
                        name: "FK_Korisnici_Uloge_IdUloga",
                        column: x => x.IdUloga,
                        principalTable: "Uloge",
                        principalColumn: "IdUloga",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifikacije",
                columns: table => new
                {
                    IdNotifikacija = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKorisnik = table.Column<int>(type: "int", nullable: false),
                    Naslov = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tekst = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifikacije", x => x.IdNotifikacija);
                    table.ForeignKey(
                        name: "FK_Notifikacije_Korisnici_IdKorisnik",
                        column: x => x.IdKorisnik,
                        principalTable: "Korisnici",
                        principalColumn: "IdKorisnik",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Poruke",
                columns: table => new
                {
                    IdPoruka = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPosiljalac = table.Column<int>(type: "int", nullable: false),
                    IdPrimalac = table.Column<int>(type: "int", nullable: false),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VrijemeSlanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Procitana = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poruke", x => x.IdPoruka);
                    table.ForeignKey(
                        name: "FK_Poruke_Korisnici_IdPosiljalac",
                        column: x => x.IdPosiljalac,
                        principalTable: "Korisnici",
                        principalColumn: "IdKorisnik",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Poruke_Korisnici_IdPrimalac",
                        column: x => x.IdPrimalac,
                        principalTable: "Korisnici",
                        principalColumn: "IdKorisnik",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Smjestaji",
                columns: table => new
                {
                    IdSmjestaj = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAdmin = table.Column<int>(type: "int", nullable: false),
                    IdGrad = table.Column<int>(type: "int", nullable: false),
                    IdTip = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BrojOsoba = table.Column<int>(type: "int", nullable: false),
                    CijenaPoNoci = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Aktivan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smjestaji", x => x.IdSmjestaj);
                    table.ForeignKey(
                        name: "FK_Smjestaji_Gradovi_IdGrad",
                        column: x => x.IdGrad,
                        principalTable: "Gradovi",
                        principalColumn: "IdGrad",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Smjestaji_Korisnici_IdAdmin",
                        column: x => x.IdAdmin,
                        principalTable: "Korisnici",
                        principalColumn: "IdKorisnik",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Smjestaji_TipoviSmjestaja_IdTip",
                        column: x => x.IdTip,
                        principalTable: "TipoviSmjestaja",
                        principalColumn: "IdTipSmjestaja",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ListaZelja",
                columns: table => new
                {
                    IdListaZelja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKorisnik = table.Column<int>(type: "int", nullable: false),
                    IdSmjestaj = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaZelja", x => x.IdListaZelja);
                    table.ForeignKey(
                        name: "FK_ListaZelja_Korisnici_IdKorisnik",
                        column: x => x.IdKorisnik,
                        principalTable: "Korisnici",
                        principalColumn: "IdKorisnik",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListaZelja_Smjestaji_IdSmjestaj",
                        column: x => x.IdSmjestaj,
                        principalTable: "Smjestaji",
                        principalColumn: "IdSmjestaj",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recenzije",
                columns: table => new
                {
                    IdRecenzija = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKorisnik = table.Column<int>(type: "int", nullable: false),
                    IdSmjestaj = table.Column<int>(type: "int", nullable: false),
                    Ocjena = table.Column<int>(type: "int", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzije", x => x.IdRecenzija);
                    table.ForeignKey(
                        name: "FK_Recenzije_Korisnici_IdKorisnik",
                        column: x => x.IdKorisnik,
                        principalTable: "Korisnici",
                        principalColumn: "IdKorisnik",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recenzije_Smjestaji_IdSmjestaj",
                        column: x => x.IdSmjestaj,
                        principalTable: "Smjestaji",
                        principalColumn: "IdSmjestaj",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacije",
                columns: table => new
                {
                    IdRezervacija = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSmjestaj = table.Column<int>(type: "int", nullable: false),
                    IdKorisnik = table.Column<int>(type: "int", nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: false),
                    DatumPrijave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumOdjave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojOsoba = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacije", x => x.IdRezervacija);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Korisnici_IdKorisnik",
                        column: x => x.IdKorisnik,
                        principalTable: "Korisnici",
                        principalColumn: "IdKorisnik",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Smjestaji_IdSmjestaj",
                        column: x => x.IdSmjestaj,
                        principalTable: "Smjestaji",
                        principalColumn: "IdSmjestaj",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezervacije_StatusiRezervacija_IdStatus",
                        column: x => x.IdStatus,
                        principalTable: "StatusiRezervacija",
                        principalColumn: "IdStatus",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SmjestajSadrzaji",
                columns: table => new
                {
                    IdSmjestaj = table.Column<int>(type: "int", nullable: false),
                    IdSadrzaj = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmjestajSadrzaji", x => new { x.IdSmjestaj, x.IdSadrzaj });
                    table.ForeignKey(
                        name: "FK_SmjestajSadrzaji_Sadrzaji_IdSadrzaj",
                        column: x => x.IdSadrzaj,
                        principalTable: "Sadrzaji",
                        principalColumn: "IdSadrzaj",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SmjestajSadrzaji_Smjestaji_IdSmjestaj",
                        column: x => x.IdSmjestaj,
                        principalTable: "Smjestaji",
                        principalColumn: "IdSmjestaj",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_IdUloga",
                table: "Korisnici",
                column: "IdUloga");

            migrationBuilder.CreateIndex(
                name: "IX_ListaZelja_IdKorisnik",
                table: "ListaZelja",
                column: "IdKorisnik");

            migrationBuilder.CreateIndex(
                name: "IX_ListaZelja_IdSmjestaj",
                table: "ListaZelja",
                column: "IdSmjestaj");

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacije_IdKorisnik",
                table: "Notifikacije",
                column: "IdKorisnik");

            migrationBuilder.CreateIndex(
                name: "IX_Poruke_IdPosiljalac",
                table: "Poruke",
                column: "IdPosiljalac");

            migrationBuilder.CreateIndex(
                name: "IX_Poruke_IdPrimalac",
                table: "Poruke",
                column: "IdPrimalac");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_IdKorisnik",
                table: "Recenzije",
                column: "IdKorisnik");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_IdSmjestaj",
                table: "Recenzije",
                column: "IdSmjestaj");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_IdKorisnik",
                table: "Rezervacije",
                column: "IdKorisnik");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_IdSmjestaj",
                table: "Rezervacije",
                column: "IdSmjestaj");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_IdStatus",
                table: "Rezervacije",
                column: "IdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Smjestaji_IdAdmin",
                table: "Smjestaji",
                column: "IdAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_Smjestaji_IdGrad",
                table: "Smjestaji",
                column: "IdGrad");

            migrationBuilder.CreateIndex(
                name: "IX_Smjestaji_IdTip",
                table: "Smjestaji",
                column: "IdTip");

            migrationBuilder.CreateIndex(
                name: "IX_SmjestajSadrzaji_IdSadrzaj",
                table: "SmjestajSadrzaji",
                column: "IdSadrzaj");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListaZelja");

            migrationBuilder.DropTable(
                name: "Notifikacije");

            migrationBuilder.DropTable(
                name: "Poruke");

            migrationBuilder.DropTable(
                name: "Recenzije");

            migrationBuilder.DropTable(
                name: "Rezervacije");

            migrationBuilder.DropTable(
                name: "SmjestajSadrzaji");

            migrationBuilder.DropTable(
                name: "StatusiRezervacija");

            migrationBuilder.DropTable(
                name: "Sadrzaji");

            migrationBuilder.DropTable(
                name: "Smjestaji");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "TipoviSmjestaja");

            migrationBuilder.DropTable(
                name: "Uloge");
        }
    }
}
