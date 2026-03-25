using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartFYPHandler.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserForGoogleAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "GoogleId",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1387));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1391));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1393));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1394));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1396));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1231), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1233) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1240), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1243), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1243) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1245), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1246) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1248), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1248) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1472), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1472) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1477), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1477) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1479), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1480) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1481), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1482) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1483), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1489) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1491), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1491) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1493), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1493) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "GoogleId", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 397, DateTimeKind.Utc).AddTicks(6656), null, "$2a$11$uMTSblz1Kb2NdU3ra2.YNuA73RfefRbrgtNjS6S5lxbbTVjcSKZ4S", new DateTime(2026, 3, 25, 14, 16, 0, 397, DateTimeKind.Utc).AddTicks(6663) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1932));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1935));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1936));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1937));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1938));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1826), new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1830) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1834), new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1835) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1836), new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1836) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1838), new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1839) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1840), new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1955), new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1956) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1959), new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1959) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1960), new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1961) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1962), new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1962) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1963), new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1969) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1970), new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1971) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1972), new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1972) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 27, 13, 357, DateTimeKind.Utc).AddTicks(4919), "$2a$11$tPLVYPlKbef.2eJ0qpHO/uLQAp0Y04GkKRZ4uf/9iHB8rG5O5XNoW", new DateTime(2026, 1, 17, 16, 27, 13, 357, DateTimeKind.Utc).AddTicks(4923) });
        }
    }
}
