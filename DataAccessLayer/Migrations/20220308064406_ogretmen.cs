using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class ogretmen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OgretmenId",
                table: "Ogrenciler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ogretmenler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogretmenler", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_OgretmenId",
                table: "Ogrenciler",
                column: "OgretmenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenciler_Ogretmenler_OgretmenId",
                table: "Ogrenciler",
                column: "OgretmenId",
                principalTable: "Ogretmenler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenciler_Ogretmenler_OgretmenId",
                table: "Ogrenciler");

            migrationBuilder.DropTable(
                name: "Ogretmenler");

            migrationBuilder.DropIndex(
                name: "IX_Ogrenciler_OgretmenId",
                table: "Ogrenciler");

            migrationBuilder.DropColumn(
                name: "OgretmenId",
                table: "Ogrenciler");
        }
    }
}
