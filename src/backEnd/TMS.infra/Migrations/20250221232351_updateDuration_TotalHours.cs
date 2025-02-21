using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tms.infra.Migrations
{
    /// <inheritdoc />
    public partial class updateDuration_TotalHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Trips",
                newName: "TotalTripHours");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6482628+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6482938+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6482993+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483044+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483096+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483159+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483209+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483259+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483305+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483362+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483413+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483467+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483522+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483573+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483622+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483672+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483726+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483803+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 19,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483854+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 20,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483903+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 21,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6483952+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 22,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484004+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 23,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484052+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 24,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484100+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 25,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484150+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 26,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484197+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 27,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484249+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 28,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484296+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 29,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484344+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 30,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484391+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 31,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484440+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 32,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484488+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 33,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484538+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 34,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484590+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 35,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484644+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 36,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484693+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 37,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484739+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 38,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484809+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 39,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484861+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 40,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484911+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 41,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6484964+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 42,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6485015+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 43,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6485068+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 44,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6485120+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 45,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6485167+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 46,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6485215+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 47,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6485265+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 48,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6485315+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 49,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6485361+00:00");

            migrationBuilder.UpdateData(
                table: "Event_Codes",
                keyColumn: "Id",
                keyValue: -4,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6434329+00:00");

            migrationBuilder.UpdateData(
                table: "Event_Codes",
                keyColumn: "Id",
                keyValue: -3,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6434268+00:00");

            migrationBuilder.UpdateData(
                table: "Event_Codes",
                keyColumn: "Id",
                keyValue: -2,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6434188+00:00");

            migrationBuilder.UpdateData(
                table: "Event_Codes",
                keyColumn: "Id",
                keyValue: -1,
                column: "Date_Created",
                value: "2025-02-21T23:23:49.6433945+00:00");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalTripHours",
                table: "Trips",
                newName: "Duration");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3070845+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071043+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071103+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071148+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071192+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071238+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071278+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071323+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071366+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071416+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071463+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071510+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071557+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071599+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071642+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071688+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071734+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071808+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 19,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071852+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 20,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071892+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 21,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071933+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 22,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3071976+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 23,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072021+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 24,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072060+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 25,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072102+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 26,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072143+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 27,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072185+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 28,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072225+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 29,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072264+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 30,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072302+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 31,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072344+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 32,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072382+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 33,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072423+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 34,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072471+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 35,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072513+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 36,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072558+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 37,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072602+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 38,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072667+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 39,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072711+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 40,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072750+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 41,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072788+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 42,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072832+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 43,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072882+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 44,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072928+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 45,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3072971+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 46,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3073010+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 47,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3073054+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 48,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3073093+00:00");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 49,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3073132+00:00");

            migrationBuilder.UpdateData(
                table: "Event_Codes",
                keyColumn: "Id",
                keyValue: -4,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3047811+00:00");

            migrationBuilder.UpdateData(
                table: "Event_Codes",
                keyColumn: "Id",
                keyValue: -3,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3047777+00:00");

            migrationBuilder.UpdateData(
                table: "Event_Codes",
                keyColumn: "Id",
                keyValue: -2,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3047737+00:00");

            migrationBuilder.UpdateData(
                table: "Event_Codes",
                keyColumn: "Id",
                keyValue: -1,
                column: "Date_Created",
                value: "2025-02-21T23:10:51.3047621+00:00");
        }
    }
}
