using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartFYPHandler.Migrations
{
    /// <inheritdoc />
    public partial class AddProgressDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProgressDetails",
                table: "FYPProjects",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6144));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6150));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6153));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6154));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6156));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(5830), new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(5833) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(5842), new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(5842) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(5846), new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(5846) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(5849), new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(5849) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(5852), new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(5853) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6209), new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6217) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6300), new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6300) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6304), new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6304) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6307), new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6307) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6309), new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6310) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6312), new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6312) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6314), new DateTime(2026, 4, 3, 20, 33, 50, 256, DateTimeKind.Utc).AddTicks(6315) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 457, DateTimeKind.Utc).AddTicks(5559), "$2a$11$LEc260i0GRq.9eOeHQTg/OxBXxOggTJ0g.KARyRvIndgq5VP0i2fa", new DateTime(2026, 4, 3, 20, 33, 50, 457, DateTimeKind.Utc).AddTicks(5565) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 200,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 657, DateTimeKind.Utc).AddTicks(5116), "$2a$11$cAS.QoyvQsfv3iYPSFxU1OAieAyAW2E3OEPDC9quJ3nJ2KlPQyHPW", new DateTime(2026, 4, 3, 20, 33, 50, 657, DateTimeKind.Utc).AddTicks(5122) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 300,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 20, 33, 50, 858, DateTimeKind.Utc).AddTicks(1331), "$2a$11$99hQyJDKHRZpbcZMxVjZEuLDtPy9v3w4FNAYqUGkdjiOxEV38kaF6", new DateTime(2026, 4, 3, 20, 33, 50, 858, DateTimeKind.Utc).AddTicks(1340) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgressDetails",
                table: "FYPProjects");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3732));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3736));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3738));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3739));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3741));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3537), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3548), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3548) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3551), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3551) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3553), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3554) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3555), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3556) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3775), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3780) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3785), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3786) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3788), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3789) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3791), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3791) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3793), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3793) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3795), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3795) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3797), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3797) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 696, DateTimeKind.Utc).AddTicks(1518), "$2a$11$UQeKMiijVRCIGDobjUNuC./Y314NuaD/T.DFHgkUoeH6qIrUlgS0O", new DateTime(2026, 3, 27, 15, 53, 41, 696, DateTimeKind.Utc).AddTicks(1522) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 200,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 900, DateTimeKind.Utc).AddTicks(8050), "$2a$11$hX4eigjSl5pSCSizAMF5ru9LE7Qz18YyKGruWd8yiubdjcApfTcTa", new DateTime(2026, 3, 27, 15, 53, 41, 900, DateTimeKind.Utc).AddTicks(8056) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 300,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 42, 123, DateTimeKind.Utc).AddTicks(9066), "$2a$11$VijOPZcWck8uoAJZHYiMGOyZIOEduHX1wKMzeTjTgoDtp57jw1OHG", new DateTime(2026, 3, 27, 15, 53, 42, 123, DateTimeKind.Utc).AddTicks(9071) });
        }
    }
}
