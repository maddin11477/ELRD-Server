using Microsoft.EntityFrameworkCore.Migrations;

namespace ELRDDataAccessLibrary.Migrations
{
    public partial class Added_BaseUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Callsign = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CrewCount = table.Column<int>(type: "int", nullable: false),
                    UnitTye = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseUnits", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseUnits");
        }
    }
}
