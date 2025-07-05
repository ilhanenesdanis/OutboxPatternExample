using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Outbox.API.Migrations
{
    /// <inheritdoc />
    public partial class orderdatev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 5, 9, 58, 12, 450, DateTimeKind.Local).AddTicks(9190));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 5, 9, 58, 12, 450, DateTimeKind.Local).AddTicks(9190),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
