using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace tms.infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TimeZone = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Date_Created = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Event_Codes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 35, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    Date_Created = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event_Codes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EquipmentId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Origin_CityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Destination_CityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Start_Date = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    End_Date = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "TEXT", nullable: false, computedColumnSql: "CAST((julianday(End_Date || ' UTC') - julianday(Start_Date || ' UTC')) * 24 * 60 AS INTEGER)", stored: true),
                    HasIssue = table.Column<bool>(type: "INTEGER", nullable: false),
                    Completed = table.Column<bool>(type: "INTEGER", nullable: false),
                    Date_Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Cities_Destination_CityId",
                        column: x => x.Destination_CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Cities_Origin_CityId",
                        column: x => x.Origin_CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Date_Created", "Name", "TimeZone" },
                values: new object[,]
                {
                    { 1, "2025-02-20T23:37:20.1536946+00:00", "Vancouver", "Pacific Standard Time" },
                    { 2, "2025-02-20T23:37:20.1537248+00:00", "Victoria", "Pacific Standard Time" },
                    { 3, "2025-02-20T23:37:20.1537310+00:00", "Kelowna", "Pacific Standard Time" },
                    { 4, "2025-02-20T23:37:20.1537366+00:00", "Kamloops", "Pacific Standard Time" },
                    { 5, "2025-02-20T23:37:20.1537424+00:00", "Prince George", "Pacific Standard Time" },
                    { 6, "2025-02-20T23:37:20.1537487+00:00", "Calgary", "Mountain Standard Time" },
                    { 7, "2025-02-20T23:37:20.1537542+00:00", "Edmonton", "Mountain Standard Time" },
                    { 8, "2025-02-20T23:37:20.1537598+00:00", "Lethbridge", "Mountain Standard Time" },
                    { 9, "2025-02-20T23:37:20.1537651+00:00", "Red Deer", "Mountain Standard Time" },
                    { 10, "2025-02-20T23:37:20.1537715+00:00", "Fort McMurray", "Mountain Standard Time" },
                    { 11, "2025-02-20T23:37:20.1537990+00:00", "Regina", "Canada Central Standard Time" },
                    { 12, "2025-02-20T23:37:20.1538056+00:00", "Saskatoon", "Canada Central Standard Time" },
                    { 13, "2025-02-20T23:37:20.1538119+00:00", "Moose Jaw", "Canada Central Standard Time" },
                    { 14, "2025-02-20T23:37:20.1538172+00:00", "Brandon", "Central Standard Time" },
                    { 15, "2025-02-20T23:37:20.1538225+00:00", "Winnipeg", "Central Standard Time" },
                    { 16, "2025-02-20T23:37:20.1538284+00:00", "Thunder Bay", "Eastern Standard Time" },
                    { 17, "2025-02-20T23:37:20.1538343+00:00", "Sault Ste. Marie", "Eastern Standard Time" },
                    { 18, "2025-02-20T23:37:20.1538404+00:00", "Sudbury", "Eastern Standard Time" },
                    { 19, "2025-02-20T23:37:20.1538455+00:00", "North Bay", "Eastern Standard Time" },
                    { 20, "2025-02-20T23:37:20.1538507+00:00", "Barrie", "Eastern Standard Time" },
                    { 21, "2025-02-20T23:37:20.1538557+00:00", "Toronto", "Eastern Standard Time" },
                    { 22, "2025-02-20T23:37:20.1538613+00:00", "Mississauga", "Eastern Standard Time" },
                    { 23, "2025-02-20T23:37:20.1538668+00:00", "Hamilton", "Eastern Standard Time" },
                    { 24, "2025-02-20T23:37:20.1538717+00:00", "London", "Eastern Standard Time" },
                    { 25, "2025-02-20T23:37:20.1538772+00:00", "Kitchener", "Eastern Standard Time" },
                    { 26, "2025-02-20T23:37:20.1538824+00:00", "Windsor", "Eastern Standard Time" },
                    { 27, "2025-02-20T23:37:20.1538886+00:00", "St. Catharines", "Eastern Standard Time" },
                    { 28, "2025-02-20T23:37:20.1538937+00:00", "Oshawa", "Eastern Standard Time" },
                    { 29, "2025-02-20T23:37:20.1538989+00:00", "Kingston", "Eastern Standard Time" },
                    { 30, "2025-02-20T23:37:20.1539041+00:00", "Ottawa", "Eastern Standard Time" },
                    { 31, "2025-02-20T23:37:20.1539200+00:00", "Gatineau", "Eastern Standard Time" },
                    { 32, "2025-02-20T23:37:20.1539260+00:00", "Montreal", "Eastern Standard Time" },
                    { 33, "2025-02-20T23:37:20.1539317+00:00", "Quebec City", "Eastern Standard Time" },
                    { 34, "2025-02-20T23:37:20.1539381+00:00", "Sherbrooke", "Eastern Standard Time" },
                    { 35, "2025-02-20T23:37:20.1539439+00:00", "Trois-Rivières", "Eastern Standard Time" },
                    { 36, "2025-02-20T23:37:20.1539494+00:00", "Saguenay", "Eastern Standard Time" },
                    { 37, "2025-02-20T23:37:20.1539550+00:00", "Rimouski", "Eastern Standard Time" },
                    { 38, "2025-02-20T23:37:20.1539607+00:00", "Edmundston", "Atlantic Standard Time" },
                    { 39, "2025-02-20T23:37:20.1539664+00:00", "Fredericton", "Atlantic Standard Time" },
                    { 40, "2025-02-20T23:37:20.1539711+00:00", "Moncton", "Atlantic Standard Time" },
                    { 41, "2025-02-20T23:37:20.1539767+00:00", "Saint John", "Atlantic Standard Time" },
                    { 42, "2025-02-20T23:37:20.1539823+00:00", "Bathurst", "Atlantic Standard Time" },
                    { 43, "2025-02-20T23:37:20.1539884+00:00", "Charlottetown", "Atlantic Standard Time" },
                    { 44, "2025-02-20T23:37:20.1539941+00:00", "Summerside", "Atlantic Standard Time" },
                    { 45, "2025-02-20T23:37:20.1539996+00:00", "Sydney", "Atlantic Standard Time" },
                    { 46, "2025-02-20T23:37:20.1540048+00:00", "Truro", "Atlantic Standard Time" },
                    { 47, "2025-02-20T23:37:20.1540101+00:00", "New Glasgow", "Atlantic Standard Time" },
                    { 48, "2025-02-20T23:37:20.1540156+00:00", "Dartmouth", "Atlantic Standard Time" },
                    { 49, "2025-02-20T23:37:20.1540208+00:00", "Halifax", "Atlantic Standard Time" }
                });

            migrationBuilder.InsertData(
                table: "Event_Codes",
                columns: new[] { "Id", "Code", "Date_Created", "Description", "Name" },
                values: new object[,]
                {
                    { -4, "Z", "2025-02-20T23:37:20.1493846+00:00", "Railcar equipment is placed at destination", "Placed" },
                    { -3, "D", "2025-02-20T23:37:20.1493786+00:00", "Railcar equipment departs from a city on route", "Departed" },
                    { -2, "A", "2025-02-20T23:37:20.1493709+00:00", "Railcar equipment arrives at a city on route", "Arrived" },
                    { -1, "W", "2025-02-20T23:37:20.1493448+00:00", "Railcar equipment is released from origin", "Released" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_Destination_CityId",
                table: "Trips",
                column: "Destination_CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_Origin_CityId",
                table: "Trips",
                column: "Origin_CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event_Codes");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
