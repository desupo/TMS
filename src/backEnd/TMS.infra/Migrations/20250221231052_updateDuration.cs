using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tms.infra.Migrations
{
    /// <inheritdoc />
    public partial class updateDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Duration",
                table: "Trips",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "TEXT",
                oldComputedColumnSql: "CAST((julianday(End_Date || ' UTC') - julianday(Start_Date || ' UTC')) * 24 * 60 AS INTEGER)");

        }
    }
}
