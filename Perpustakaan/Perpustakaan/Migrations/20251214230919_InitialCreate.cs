using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perpustakaan.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryBuku",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BukuId = table.Column<int>(type: "int", nullable: false),
                    Aksi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Waktu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataSebelum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataSesudah = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryBuku", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterJenisBuku",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JenisBuku = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterJenisBuku", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buku",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Judul = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Penulis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JenisBukuId = table.Column<int>(type: "int", nullable: false),
                    TanggalRilis = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JumlahHalaman = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buku", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buku_MasterJenisBuku_JenisBukuId",
                        column: x => x.JenisBukuId,
                        principalTable: "MasterJenisBuku",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buku_JenisBukuId",
                table: "Buku",
                column: "JenisBukuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buku");

            migrationBuilder.DropTable(
                name: "HistoryBuku");

            migrationBuilder.DropTable(
                name: "MasterJenisBuku");
        }
    }
}
