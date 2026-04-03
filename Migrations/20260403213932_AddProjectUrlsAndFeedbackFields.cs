using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartFYPHandler.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectUrlsAndFeedbackFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DemoUrl",
                table: "FYPProjects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentUrl",
                table: "FYPProjects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Features",
                table: "FYPProjects",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GithubUrl",
                table: "FYPProjects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Technologies",
                table: "FYPProjects",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1899));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1907));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1912));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1916));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1919));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1242), new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1247) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1261), new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1262) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1375), new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1376) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1381), new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1382) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1387), new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1388) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(1998), new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(2018) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(2027), new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(2028) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(2034), new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(2035) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(2039), new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(2040) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(2044), new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(2045) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(2049), new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(2050) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(2055), new DateTime(2026, 4, 3, 21, 39, 30, 35, DateTimeKind.Utc).AddTicks(2056) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 30, 401, DateTimeKind.Utc).AddTicks(7020), "$2a$11$3/p9.6iG6m7HHd8RsFf5SO66kCk9Rh4mDTIVPRVMrc8XpsGhqzIoi", new DateTime(2026, 4, 3, 21, 39, 30, 401, DateTimeKind.Utc).AddTicks(7032) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 200,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 30, 766, DateTimeKind.Utc).AddTicks(4953), "$2a$11$5l427U21CFCurFjdRVsrUeI214jFKpb9NYWzev4kXks1/5elUwphy", new DateTime(2026, 4, 3, 21, 39, 30, 766, DateTimeKind.Utc).AddTicks(4963) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 300,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 21, 39, 31, 128, DateTimeKind.Utc).AddTicks(2674), "$2a$11$pa/8FEy5CvOhLy0eYL0tPOmX5AKUsCfUeWAVtZixcvelG5r2.OwiK", new DateTime(2026, 4, 3, 21, 39, 31, 128, DateTimeKind.Utc).AddTicks(2685) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DemoUrl",
                table: "FYPProjects");

            migrationBuilder.DropColumn(
                name: "DocumentUrl",
                table: "FYPProjects");

            migrationBuilder.DropColumn(
                name: "Features",
                table: "FYPProjects");

            migrationBuilder.DropColumn(
                name: "GithubUrl",
                table: "FYPProjects");

            migrationBuilder.DropColumn(
                name: "Technologies",
                table: "FYPProjects");

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
    }
}
