using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_Projekt_Wypozyczalnia.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klienci",
                columns: table => new
                {
                    IDKlienta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumerDokumentu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypDokumentu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresZamieszkania = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klienci", x => x.IDKlienta);
                });

            migrationBuilder.CreateTable(
                name: "Samochody",
                columns: table => new
                {
                    IDSamochodu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rok = table.Column<int>(type: "int", nullable: false),
                    NumerRejestracyjny = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Przebieg = table.Column<int>(type: "int", nullable: false),
                    TerminPrzegladu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminUbezpieczenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PojemnoscSilnika = table.Column<int>(type: "int", nullable: false),
                    RodzajPaliwa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samochody", x => x.IDSamochodu);
                });

            migrationBuilder.CreateTable(
                name: "PozycjeWypozyczenia",
                columns: table => new
                {
                    IDWypozyczenia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDKlienta = table.Column<int>(type: "int", nullable: false),
                    IDSamochodu = table.Column<int>(type: "int", nullable: false),
                    DataWypozyczenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataZwrotu = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataPrzewidywanaZwrotu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusWynajmu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PozycjeWypozyczenia", x => x.IDWypozyczenia);
                    table.ForeignKey(
                        name: "FK_PozycjeWypozyczenia_Klienci_IDKlienta",
                        column: x => x.IDKlienta,
                        principalTable: "Klienci",
                        principalColumn: "IDKlienta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PozycjeWypozyczenia_Samochody_IDSamochodu",
                        column: x => x.IDSamochodu,
                        principalTable: "Samochody",
                        principalColumn: "IDSamochodu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PozycjeWypozyczenia_IDKlienta",
                table: "PozycjeWypozyczenia",
                column: "IDKlienta");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjeWypozyczenia_IDSamochodu",
                table: "PozycjeWypozyczenia",
                column: "IDSamochodu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PozycjeWypozyczenia");

            migrationBuilder.DropTable(
                name: "Klienci");

            migrationBuilder.DropTable(
                name: "Samochody");
        }
    }
}
